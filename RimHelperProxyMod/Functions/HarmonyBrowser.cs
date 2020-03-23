using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Harmony;
using IPCInterface;
using IPCInterface.HarmonyBrowser;
using RimHelperProxyMod.Extensions;
using RimHelperProxyMod.Functions.Profiling;
using Verse;

namespace RimHelperProxyMod.Functions
{
    public static class HarmonyBrowser
    {
        private const string HarmonyBrowserId = "harmony.pirateby.browser";

        private static HashSet<MethodBase> _patched = new HashSet<MethodBase>(); // prevent multi patch
        private static HarmonyInstance _harmony = HarmonyInstance.Create(HarmonyBrowserId);

        private static readonly MethodInfo _patchStartTranspiler = AccessTools.Method(typeof(HarmonyProfiler), nameof(HarmonyProfiler.Patch_Start_Transpiler));
        private static readonly MethodInfo _patchStart = AccessTools.Method(typeof(HarmonyProfiler), nameof(HarmonyProfiler.Patch_Start));
        private static readonly MethodInfo _patchStop = AccessTools.Method(typeof(HarmonyProfiler), nameof(HarmonyProfiler.Patch_Stop));

        private static readonly HarmonyMethod MethodPrefix, MethodPostfix, PatchPrefix, PatchPostfix, PatchTranspiler;

        static HarmonyBrowser()
        {
            MethodPrefix = new HarmonyMethod(_patchStart) {prioritiy = int.MaxValue};
            MethodPostfix = new HarmonyMethod(_patchStop) {prioritiy = int.MinValue};
            PatchPrefix = new HarmonyMethod(_patchStart) {prioritiy = int.MinValue};
            PatchTranspiler = new HarmonyMethod(_patchStartTranspiler) {prioritiy = int.MinValue};
            PatchPostfix = new HarmonyMethod(_patchStop) {prioritiy = int.MaxValue};
        }

        public static bool TryAddProfiler(MethodBase method, bool isPatch = false)
        {
            if (_patched.Add(method) && method != _patchStart && method != _patchStop && method != _patchStartTranspiler)
            {
                if (isPatch)
                {
                    // setting transpilers slow like shit!
                    _harmony.Patch(method, /*transpiler: PatchTranspiler*/prefix: PatchPrefix, postfix: PatchPostfix);
                }
                else
                {
                    _harmony.Patch(method, prefix: MethodPrefix, postfix: MethodPostfix);
                }

                return true;
            }

            return false;
        }

        public static HarmonyInstances GetHarmonyInstances()
        {
            var result = new HarmonyInstances();
            var resultList = result.List;
            var patched = _harmony.GetPatchedMethods();
            foreach (var method in patched)
            {
                var patchInfo = _harmony.GetPatchInfo(method);
                foreach (var owner in patchInfo.Owners)
                {
                    if (!resultList.Contains(owner))
                    {
                        resultList.Add(owner);
                    }
                }
            }

            return result;
        }

        // notepad patches output
        public static string GetAllHarmonyPatches()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("===[Patch.Owners > 1]===");
            sb.AppendLine("===[Can conflict patches]===");
            DumpHarmonyPatches(sb, true);

            sb.AppendLine("===[Patch.Owners == 1]===");
            sb.AppendLine("===[Other patches]===");
            DumpHarmonyPatches(sb, false);

            return sb.ToString();
        }

        // unpatch local(profiler) patches
        public static void UnpatchAll()
        {
            HarmonyProfiler.Active = false;

            _harmony.UnpatchAll(HarmonyBrowserId);
            HarmonyProfiler.Reset();
            _patched.Clear();
        }

        public static List<Pair> GetHarmonyPatchesForInstances(List<string> instances)
        {
            var result = new List<Pair>();
            var patched = _harmony.GetPatchedMethods();
            foreach (var m in patched)
            {
                var p = _harmony.GetPatchInfo(m);
                foreach (var owner in p.Owners)
                {
                    if (instances.Contains(owner))
                    {
                        result.Add(new Pair {Key = owner, Value = m.GetMethodNameString() });
                        break;
                    }
                }
            }

            return result;
        }

        // instances contain {name, list<patches>} if list<patches> null, remove all patches for this instance
        public static void Unpatch(List<HarmonyUnpatch> instances)
        {
            var listPatched = _harmony.GetPatchedMethods().ToList();
            foreach (var m in listPatched)
            {
                var p = _harmony.GetPatchInfo(m);
                foreach (var o in p.Owners)
                {
                    var inst = instances.FirstOrDefault(i => i.instance == o);
                    if (inst != null && (inst.patches == null || inst.patches.Contains(m.GetMethodNameString())))
                    {
                        try
                        {
                            HarmonyInstance.Create(inst.instance).Unpatch(m, HarmonyPatchType.All, inst.instance);
                            Log.Message($"[{inst.instance}] Unpatched: {m.GetMethodNameString()}");
                        }
                        catch (Exception e)
                        {
                            Log.Message($"[{inst.instance}] Unpatch error: {e.Message}");
                            Log.Message($"{e.StackTrace}");
                        }
                        
                    }
                }
            }
        }

        public static void ResetProfilingResults()
        {
            bool oldState = HarmonyProfiler.Active;

            HarmonyProfiler.Active = false;
            HarmonyProfiler.Reset();
            HarmonyProfiler.Active = oldState;
        }

        public static List<ReportRecord> GetProfilingResults()
        {
            HarmonyProfiler.Active = false;
            List<ReportRecord> result = HarmonyProfiler.GenerateProfilingResult();
            HarmonyProfiler.Active = true;

            return result;
        }

        public static void StartProfiling()
        {
            //_harmony.UnpatchAll(HarmonyBrowserId);
            HarmonyProfiler.Active = false;
            var sw = new Stopwatch();
            sw.Start();

            FileLog.Reset();
            var listPatched = _harmony.GetPatchedMethods().ToList();
            foreach (var m in listPatched)
            {
                if (TryAddProfiler(m, false))
                    Log.Message($"Patched: {m.GetMethodFullString()}");
                else
                    FileLog.Log($"Skip: {m.GetMethodFullString()}");
            }
            FileLog.FlushBuffer();

            sw.Stop();

            HarmonyProfiler.Active = true;

            Log.Warning($"Patches initialized in {(int)Math.Round(sw.ElapsedMilliseconds / 1000f)} sec.");
        }

        public static void StartGameProfiling(string[] functions)
        {
            HarmonyProfiler.Active = false;
            var sw = new Stopwatch();
            sw.Start();

            FileLog.Reset();
            foreach (var f in functions)
            {
                var m = AccessTools.Method(f);

                if (m != null)
                {
                    if (TryAddProfiler(m, false))
                        Log.Message($"Patched: {f}");
                    else
                        FileLog.Log($"Skip: {f}");
                }
                else Log.Error($"Can't patch: {f}");
            }
            FileLog.FlushBuffer();

            sw.Stop();

            HarmonyProfiler.Active = true;

            Log.Warning($"Patches initialized in {(int)Math.Round(sw.ElapsedMilliseconds / 1000f)} sec.");
        }

        public static void StartGameProfilingTickerList()
        {
            HarmonyProfiler.Active = false;
            var sw = new Stopwatch();
            sw.Start();

            var tickManager = Traverse.Create(Find.TickManager);
            var wasAdded = new HashSet<Type>();

            void patch(string tickListField)
            {
                var tickList = tickManager.Field(tickListField);
                var thingLists = tickList.Field("thingLists").GetValue<List<List<Thing>>>();
                var tickType = tickList.Field("tickType").GetValue<TickerType>();

                void applyPatch(Type type, string tickTypeName)
                {
                    var method = AccessTools.Method(type, tickTypeName);

                    if (TryAddProfiler(method, false))
                        Log.Message($"Patched: {type}.{tickTypeName}");
                    else
                        FileLog.Log($"Skip: {type}.{tickTypeName}");
                }

                foreach (var list in thingLists)
                {
                    foreach (var thing in list)
                    {
                        var type = thing.GetType();
                        if (wasAdded.Add(type))
                        {
                            switch (tickType)
                            {
                                case TickerType.Normal:
                                    applyPatch(type, "Tick");
                                    break;
                                case TickerType.Rare:
                                    applyPatch(type, "TickRare");
                                    break;
                                case TickerType.Long:
                                    applyPatch(type, "TickLong");
                                    break;
                            }
                        }
                    }
                }
            }

            FileLog.Reset();
            patch("tickListNormal");
            patch("tickListRare");
            patch("tickListLong");
            FileLog.FlushBuffer();

            sw.Stop();

            HarmonyProfiler.Active = true;

            Log.Warning($"Patches initialized in {(int)Math.Round(sw.ElapsedMilliseconds / 1000f)} sec.");
        }

        public static void StartPatchesProfiling(HarmonyInstances hi)
        {
            HarmonyProfiler.Active = false;
            var sw = new Stopwatch();
            sw.Start();

            var transpiler = new HarmonyMethod(_patchStartTranspiler);
            var postfix = new HarmonyMethod(_patchStop);
            
            postfix.prioritiy = Int32.MaxValue;
            transpiler.prioritiy = Int32.MinValue;

            bool checkOwner(string owner) => hi.List.Count == 0 || hi.List.Contains(owner);

            bool checkContainOwner(IEnumerable<Patch> patches)
            {
                if (hi.List.Count == 0)
                    return true;

                foreach (var p in patches)
                {
                    if (hi.List.Contains(p.owner))
                    {
                        return true;
                    }
                }

                return false;
            }

            FileLog.Reset();
            var listPatched = _harmony.GetPatchedMethods().ToList();
            foreach (var m in listPatched)
            {
                var patch = _harmony.GetPatchInfo(m);

                if (hi.SkipMethodsInGenericClass && m.ReflectedType.IsGenericType)
                {
                    FileLog.Log($"Skip {m.ReflectedType.Name} is Generic");
                    continue;
                }

                void patchPatches(ReadOnlyCollection<Patch> patches)
                {
                    foreach (var p in patches)
                    {
                        if (!checkOwner(p.owner))
                            continue;

                        if (TryAddProfiler(p.patch, true))
                            Log.Message($"[{p.owner}] Patch: {m.GetMethodFullString()}");
                        else
                            FileLog.Log($"[{p.owner}] Skip_Patch: {m.GetMethodFullString()}");
                    }
                }
                
                patchPatches(patch.Prefixes);
                patchPatches(patch.Postfixes);


                if (checkContainOwner(patch.Transpilers) && TryAddProfiler(m, true))
                    Log.Message($"Transpilers: {m.GetMethodNameString()}");
                else
                    FileLog.Log($"Skip_Transpilers: {m.GetMethodFullString()}");
            }
            FileLog.FlushBuffer();

            sw.Stop();

            HarmonyProfiler.Active = true;

            Log.Warning($"Patches initialized in {(int)Math.Round(sw.ElapsedMilliseconds / 1000f)} sec.");
        }



        private static void DumpHarmonyPatches(StringBuilder sb, bool moreThanOne)
        {
            void dumpPatchesInfo(string type, ReadOnlyCollection<Patch> patches)
            {
                // Sort patches by harmony execute priority
                var patchesSorted = patches.ToList().OrderByDescending(x => x.priority).ThenBy(x => x.index);

                foreach (var p in patchesSorted)
                {
                    var m = p.patch;

                    sb.AppendLine(
                        $" {type}:{m.ReturnType.Name} {m.GetMethodFullString()} [mod:{p.owner}, prior:{p.priority}, idx:{p.index}]");

                    foreach (var b in p.before)
                        sb.AppendLine($"  before:{b}");

                    foreach (var a in p.after)
                        sb.AppendLine($"  after:{a}");
                }
                //if (patches.Count > 0)
                //    sb.AppendLine();
            }

            var listPatched = _harmony.GetPatchedMethods().ToList();
            foreach (var method in listPatched)
            {
                var patch = _harmony.GetPatchInfo(method);
                if (moreThanOne && patch.Owners.Count > 1 || !moreThanOne && patch.Owners.Count == 1)
                {
                    sb.AppendLine($"{method.GetMethodFullString()}:(Owners:{patch.Owners.Count}, Prefixes:{patch.Prefixes.Count}, Postfixes:{patch.Postfixes.Count}, Transpilers:{patch.Transpilers.Count})");
                    dumpPatchesInfo("prefix", patch.Prefixes);
                    dumpPatchesInfo("transpiler", patch.Transpilers);
                    dumpPatchesInfo("postfix", patch.Postfixes);
                    sb.AppendLine();
                }
            }
        }
    }
}