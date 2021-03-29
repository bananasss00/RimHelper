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
        GetFoods,
        GetPawnsHeddifs,
        GetWeaponsRanged,
        GetWeaponsMelee,
        GetApparels,
        GetBodyParts,
        GetFacilities,
        GetPlants,
        GetBackstorys,
        GetTraits,
        GetCEAmmos,
        GetTools,
        BuildingStuffDump,
        WeaponApparelDump,
        InjectDll,
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