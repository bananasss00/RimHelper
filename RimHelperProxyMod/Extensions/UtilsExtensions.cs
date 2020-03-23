using System.Linq;
using RimWorld;
using Verse;

namespace RimHelperProxyMod.Extensions
{
    public static class UtilsExtensions
    {
        public static float? GetStatFactorValue(this ThingDef stuff, StatDef statDef)
        {
            StatModifier factorFromDef = stuff.stuffProps?.statFactors?.FirstOrDefault(fa => fa.stat == statDef);
            return factorFromDef?.value;
        }

        public static float? GetStatOffsetValue(this ThingDef stuff, StatDef statDef)
        {
            StatModifier factorFromDef = stuff.stuffProps?.statOffsets?.FirstOrDefault(fa => fa.stat == statDef);
            return factorFromDef?.value;
        }

        //public static float GetStatValue(this Thing thing, StatDef stat, bool applyPostProcess = true)
        //{
        //    return stat.Worker.GetValue(thing, applyPostProcess);
        //}

        //public static float GetStatValueAbstract(this BuildableDef def, StatDef stat, ThingDef stuff = null)
        //{
        //    return stat.Worker.GetValueAbstract(def, stuff);
        //}

        public static float? MeleeDpsSharpFactorOverall(this ThingDef d)
        {
            float damage = d.GetStatValueAbstract(StatDefOf.SharpDamageMultiplier);
            float? cooldown = d.GetStatFactorValue(StatDefOf.MeleeWeapon_CooldownMultiplier);
            if (cooldown == null) return null;
            return (damage / cooldown).RoundTo2();
        }

        public static float? MeleeDpsBluntFactorOverall(this ThingDef d)
        {
            float damage = d.GetStatValueAbstract(StatDefOf.BluntDamageMultiplier);
            float? cooldown = d.GetStatFactorValue(StatDefOf.MeleeWeapon_CooldownMultiplier);
            if (cooldown == null) return null;
            return (damage / cooldown).RoundTo2();
        }
    }
}