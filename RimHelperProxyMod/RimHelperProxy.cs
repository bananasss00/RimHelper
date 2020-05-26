using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Harmony;
using RimHelperProxyMod.Functions;
using Verse;
using IPCInterface;
using IPCInterface.HarmonyBrowser;

namespace RimHelperProxyMod
{
    public class RimHelperProxy : Mod
    {
        public RimHelperProxy(ModContentPack content) : base(content)
        {
            IPC.Create();
            Log.Warning($"[RimHelperProxy] Shared memory with size: {IPC.MemorySize / (1024 * 1024)} Mb allocated!");

            HarmonyInstance.Create("rimworld.pirateby.mod.rimhelperproxy").PatchAll(Assembly.GetExecutingAssembly());

            new Thread(() => // Exception not handling from another threads!
            {
                while (true)
                {
                    HandleMessagesTick();
                    ResetTick();
                    Thread.Sleep(100);
                }
            }).Start(); 
        }

        public static void ResetTick()
        {
            Extensions.ThingDefExtensions.Reset();
        }

        public static void HandleMessagesTick()
        {
            if (IPC.Active)
            {
                try
                {
                    switch ((State)IPC.State)
                    {
                        case State.Idle:
                            return;
                        case State.GetMaterials:
                            IPC.SetObjectBuf(Materials.Get());
                            break;
                        case State.GetWeaponsRanged:
                            IPC.SetObjectBuf(WeaponsRanged.Get());
                            break;
                        case State.GetWeaponsMelee:
                            IPC.SetObjectBuf(WeaponsMelee.Get());
                            break;
                        case State.GetApparels:
                            IPC.SetObjectBuf(Apparels.Get());
                            break;
                        case State.GetBuildingsFromMaterials:
                            IPC.SetObjectBuf(BuildingsFromMaterials.Get());
                            break;
                        case State.GetAnimals:
                            IPC.SetObjectBuf(Animals.Get());
                            break;
                        case State.GetDebuffs:
                            IPC.SetObjectBuf(Debuffs.Get());
                            break;
                        case State.GetDrugs:
                            IPC.SetObjectBuf(Drugs.Get());
                            break;
                        case State.GetActiveIncidents:
                            IPC.StringBuf = ActiveIncidents.Get();
                            break;
                        case State.GetPawnsHeddifs:
                            IPC.StringBuf = PawnsHeddifs.Get();
                            break;
                        case State.GetBodyParts:
                            IPC.SetObjectBuf(BodyParts.Get());
                            break;
                        case State.GetFacilities:
                            IPC.SetObjectBuf(Facilities.Get());
                            break;
                        case State.GetPlants:
                            IPC.SetObjectBuf(Plants.Get());
                            break;
                        case State.GetBackstorys:
                            IPC.SetObjectBuf(Backstorys.Get());
                            break;
                        case State.GetTraits:
                            IPC.SetObjectBuf(Traits.Get());
                            break;
                        case State.GetCEAmmos:
                            IPC.SetObjectBuf(CEAmmos.Get());
                            break;
                        case State.GetTools:
                            IPC.SetObjectBuf(Tools.Get());
                            break;
                        case State.BuildingStuffDump:
                            new BuildingStuffDump();
                            break;
                        case State.WeaponApparelDump:
                            IPC.StringBuf = WeaponApparelDump.Get();
                            break;
                        case State.InjectDll:
                            IPC.StringBuf = InjectDll.GetResult(IPC.GetObjectBuf<InjectParameters>());
                            break;
                        case State.GetAllHarmonyPatches:
                            IPC.StringBuf = HarmonyBrowser.GetAllHarmonyPatches();
                            break;
                        case State.StartHarmonyProfiling:
                            HarmonyBrowser.StartProfiling();
                            break;
                        case State.StartHarmonyPatchesProfiling:
                            HarmonyBrowser.StartPatchesProfiling(IPC.GetObjectBuf<HarmonyInstances>());
                            break;
                        case State.StartGameProfiling:
                            HarmonyBrowser.StartGameProfiling(IPC.GetObjectBuf<string[]>());
                            break;
                        case State.StartGameProfilingTickerList:
                            HarmonyBrowser.StartGameProfilingTickerList();
                            break;
                        case State.GetHarmonyProfilingResults:
                            IPC.SetObjectBuf(HarmonyBrowser.GetProfilingResults());
                            break;
                        case State.ResetHarmonyProfilingResults:
                            HarmonyBrowser.ResetProfilingResults();
                            break;
                        case State.HarmonyProfilingUnpatchAll:
                            HarmonyBrowser.UnpatchAll();
                            break;
                        case State.HarmonyUnpatchInstances:
                            HarmonyBrowser.Unpatch(IPC.GetObjectBuf<List<HarmonyUnpatch>>());
                            break;
                        case State.GetHarmonyInstances:
                            IPC.SetObjectBuf(HarmonyBrowser.GetHarmonyInstances());
                            break;
                        case State.GetHarmonyPatchesForInstances:
                            IPC.SetObjectBuf(HarmonyBrowser.GetHarmonyPatchesForInstances(IPC.GetObjectBuf<List<string>>()));
                            break;
                        case State.GcCollect:
                            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                            GC.WaitForPendingFinalizers();
                            Log.Warning($"[RimHelperProxy] GC.Collect() called!");
                            break;


                        default:
                            throw new Exception($"UnknownState: {IPC.State}");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"[HandleMessagesTick] Exception: {e.Message}");
                    Log.Error($"[HandleMessagesTick] StackTrace: {e.StackTrace}");
                }
                finally
                {
                    IPC.State = (int)State.Idle;
                }
            }
        }
    }
}