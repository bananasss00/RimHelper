using System.Collections.Generic;
using System.Reflection;
using Harmony;
using Verse;

namespace RimHelperProxyMod.Harmony
{
    // prevent error spam when create graphic from another thread
    [HarmonyPatch(typeof(Pawn_AgeTracker), "RecalculateLifeStageIndex")]
    public static class Pawn_AgeTracker_Patch
    {
        public static PawnKindDef SkipNextPawnKindDef = null;

        static readonly FieldInfo Pawn = typeof(Pawn_AgeTracker).GetField("pawn", BindingFlags.Instance | BindingFlags.NonPublic);
        static readonly FieldInfo CachedLifeStageIndex = typeof(Pawn_AgeTracker).GetField("cachedLifeStageIndex", BindingFlags.Instance | BindingFlags.NonPublic);

        [HarmonyPrefix]
        static void RecalculateLifeStageIndex(Pawn_AgeTracker __instance)
        {
            if (SkipNextPawnKindDef != null)
            {
                Pawn pawn = (Pawn)Pawn.GetValue(__instance);
                if (SkipNextPawnKindDef == pawn.kindDef)
                {
                    if (PreventGraphicUpdate(__instance, pawn))
                        SkipNextPawnKindDef = null;
                }
            }
        }

        static bool PreventGraphicUpdate(Pawn_AgeTracker _this, Pawn pawn)
        {
            int num = -1;
            List<LifeStageAge> lifeStageAges = pawn.RaceProps.lifeStageAges;
            for (int i = lifeStageAges.Count - 1; i >= 0; i--) {
                if (lifeStageAges[i].minAge <= _this.AgeBiologicalYearsFloat + 1E-06f) {
                    num = i;
                    break;
                }
            }
            if (num == -1)
                num = 0;

            CachedLifeStageIndex.SetValue(_this, num);
            return num > 0;
        }
    }

    //[StaticConstructorOnStartup]
    //public static class Patches
    //{
    //    static Patches()
    //    {
    //        HarmonyInstance harmony = HarmonyInstance.Create("rimworld.pirateby.mod.rimhelperproxy");

    //        harmony.Patch(
    //            /*AccessTools.Constructor(typeof(PawnGraphicSetBlocker))*/
    //            typeof(PawnGraphicSet).GetConstructor(new Type[] { typeof(Pawn) }),
    //            prefix: new HarmonyMethod(typeof(PawnGraphicSetBlocker), nameof(PawnGraphicSetBlocker.Ctor)));
    //        harmony.Patch(
    //            AccessTools.Method(typeof(Verse.PawnGraphicSet), nameof(Verse.PawnGraphicSet.ResolveAllGraphics)),
    //            prefix: new HarmonyMethod(typeof(PawnGraphicSetBlocker), nameof(PawnGraphicSetBlocker.ResolveAllGraphics)));
    //    }
    //}

    //public static class PawnGraphicSetBlocker
    //{
    //    static Pawn filterPawn = null;
    //    static PawnKindDef filterKindDef = null;

    //    public static void FilterNextPawn(PawnKindDef kindDef) => filterKindDef = kindDef;

    //    public static void Reset()
    //    {
    //        filterPawn = null;
    //        filterKindDef = null;
    //    }

    //    public static void Ctor(PawnGraphicSet __instance, Pawn pawn)
    //    {
    //        if (filterKindDef != null && pawn.kindDef == filterKindDef)
    //        {
    //            filterPawn = pawn;
    //            filterKindDef = null;
    //        }
    //    }
    //    public static bool ResolveAllGraphics(PawnGraphicSet __instance)
    //    {
    //        Log.Error($"[ResolveAllGraphics] Pawn {__instance.pawn.def.label}");
    //        if (filterPawn == null)
    //            return true;

    //        return !(filterPawn == __instance.pawn);
    //    }
    //}
}