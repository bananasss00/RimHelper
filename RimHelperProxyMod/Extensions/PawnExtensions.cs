using System.Linq;
using RimHelperProxyMod.Harmony;
using Verse;

namespace RimHelperProxyMod.Extensions
{
    public static class PawnExtensions
    {
        public static Pawn SafeThreadGeneratePawn(this ThingDef def)
        {
            var kindDef = DefDatabase<PawnKindDef>.AllDefs.FirstOrDefault(k => k.defName == def.defName);
            Pawn_AgeTracker_Patch.SkipNextPawnKindDef = kindDef;
            return PawnGenerator.GeneratePawn(kindDef);
        }

        public static Pawn SafeThreadGeneratePawn(this PawnKindDef kindDef)
        {
            Pawn_AgeTracker_Patch.SkipNextPawnKindDef = kindDef;
            return PawnGenerator.GeneratePawn(kindDef);
        }
    }
}