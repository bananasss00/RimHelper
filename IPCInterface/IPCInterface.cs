using System;
using System.Collections.Generic;

namespace IPCInterface
{
    public enum State
    {
        Idle,
        GetActiveIncidents,
        GetMaterials,
        GetBuildingsFromMaterials,
        GetAnimals,
        GetDebuffs,
        GetDrugs,
        GetPawnsHeddifs,
        GetWeaponsRanged,
        GetWeaponsMelee,
        GetApparels,
        GetBodyParts,
        GetFacilities,
        GetCEAmmos,
        GetTools,
        BuildingStuffDump,
        WeaponApparelDump,
        InjectDll,
// harmony profiler commands //
        GetAllHarmonyPatches,
        StartHarmonyProfiling,
        StartHarmonyPatchesProfiling,
        StartGameProfiling,
        StartGameProfilingTickerList,
        GetHarmonyProfilingResults,
        ResetHarmonyProfilingResults,
        HarmonyProfilingUnpatchAll,
        HarmonyUnpatchInstances,
        GetHarmonyInstances,
        GetHarmonyPatchesForInstances,
        GcCollect
    }

    [Serializable]
    public class InjectParameters
    {
        public string dllPath;
        public string dllClass;
        public string dllFunction;
    }
}