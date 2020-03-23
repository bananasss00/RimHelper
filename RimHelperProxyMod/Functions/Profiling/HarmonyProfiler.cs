using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Harmony;
using IPCInterface.HarmonyBrowser;
using Verse;

namespace RimHelperProxyMod.Functions.Profiling
{
    public class StopwatchRecord
    {
        public Stopwatch watch = new Stopwatch();
        public long avg = 0;
        public long num = 0;
        public long min = long.MaxValue;
        public long max = 0;

        public void Start()
        {
            watch.Reset();
            watch.Start();
        }

        public void Stop()
        {
            if (watch.IsRunning)
            {
                watch.Stop();

                long ticks = watch.ElapsedTicks;
                avg = (long)Math.Round((double)(avg * num + ticks) / (num + 1));
                num++;

                min = ticks < min ? ticks : min;
                max = ticks > max ? ticks : max;
            }
        }
    }

    public static class HarmonyProfiler
    {
        public static bool Active { get; set; } = false;

        public static void Reset() => ProfiledMethods.Clear();

        public static Dictionary<MethodBase, StopwatchRecord> ProfiledMethods { get; } = new Dictionary<MethodBase, StopwatchRecord>();

        private static readonly MethodInfo MethodPatchStart = typeof(HarmonyProfiler).GetMethod("Patch_Start");

        // MethodInfo __originalMethod in patches cause exception if patch in generic class
        // then get from stack
        #region HarmonyPatches
        /*
         * Frames:
         * 0 - GetPatchedMethod
         * 1 - Patch_Start/Stop
         * 2 - Where was call Patch_Start/Stop
         */
        public static MethodBase GetPatchedMethod() => new StackTrace(true).GetFrame(2).GetMethod();

        public static StopwatchRecord GetStopwatchRecord(MethodBase method)
        {
            if (!ProfiledMethods.ContainsKey(method))
                ProfiledMethods.Add(method, new StopwatchRecord());
            return ProfiledMethods[method];
        }

        public static IEnumerable<CodeInstruction> Patch_Start_Transpiler(IEnumerable<CodeInstruction> code)
        {
            var codeList = code.ToList();
            codeList.Insert(0, new CodeInstruction(OpCodes.Call, MethodPatchStart));
            return codeList;
        }
        
        // Start timer
        public static void Patch_Start()
        {
            if (!Active)
                return;

            MethodBase method = GetPatchedMethod();
            GetStopwatchRecord(method).Start();
        }

        // Stop timer and calc avg time
        public static void Patch_Stop()
        {
            if (!Active)
                return;

            MethodBase method = GetPatchedMethod();
            GetStopwatchRecord(method).Stop();
        }
        #endregion

        public static List<ReportRecord> ConvertToReportRecords(this Dictionary<MethodBase, StopwatchRecord> profiledRecords)
        {
            List<ReportRecord> report = new List<ReportRecord>();

            foreach (var record in profiledRecords)
            {
                MethodBase m = record.Key;
                string name = $"{m.ReflectedType.Name}.{m.Name}";

                int idx = name.LastIndexOf("_Patch");

                if (idx != -1)
                    name = name.Substring(0, name.LastIndexOf("_Patch"));
                else
                    Log.Error($"LastIndexOf = -1: {name}({m.ReflectedType.Assembly.GetName().Name})");

                string asm = $"{m.ReflectedType.Assembly.GetName().Name}";

                StopwatchRecord rec = record.Value;
                double sumTicks = rec.avg * rec.num;
                long avgTimeTick = (long)Math.Round((sumTicks / Stopwatch.Frequency) * 1000L);
                report.Add(new ReportRecord
                {
                    name = name,
                    asm = asm,
                    avgTime = rec.avg,
                    min = rec.min,
                    max = rec.max,
                    num = rec.num,
                    avgTimeTick = avgTimeTick
                });
            }

            return report;
        }

        public static List<ReportRecord> GenerateProfilingResult()
        {
            StringBuilder sb = new StringBuilder();

            List<ReportRecord> report = ProfiledMethods.ConvertToReportRecords();
            report.SortByDescending(x => x.avgTime);

            return report;
        }

        public static string GenerateProfilingResultString()
        {
            StringBuilder sb = new StringBuilder();

            // Create list records and sort by avgTime
            List<ReportRecord> report = ProfiledMethods.ConvertToReportRecords();
            report.SortByDescending(x => x.avgTimeTick);

            // Generate sorted output report
            sb.AppendLine($"{"PatchName", -90} - {"avgTime(ticks)", -20} {"min/max"}");
            foreach (var record in report)
            {
                sb.AppendLine($"{record.name + "(" + record.asm + ")",-90} - {record.avgTime + "(" + record.num + ")",-20} {record.min}/{record.max}");
            }

            return sb.ToString();
        }
    }
}