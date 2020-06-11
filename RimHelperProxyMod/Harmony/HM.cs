using System.Collections.Generic;
using System.Reflection;
using Harmony;
using Verse;

namespace RimHelperProxyMod.Harmony
{
    public class HM
    {
        private static HarmonyInstance _instance;

        public static HarmonyInstance Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = HarmonyInstance.Create("rimworld.pirateby.mod.rimhelperproxy");
                }

                return _instance;
            }
        }
        public static void Init()
        {
            Instance.PatchAll(Assembly.GetExecutingAssembly());
        }
        
    }

    // DisableGraphicLoad - prevent spam errors. texture created in another thread
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
}