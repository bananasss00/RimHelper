using System.Linq;
using RimHelperProxyMod.Harmony;
using RimWorld;
using Verse;
using Verse.AI;

namespace RimHelperProxyMod.Extensions
{
    public static class AnimalExtensions
    {
        public static float AnimalMeleeDps(this ThingDef def)
        {
            if (def.tools == null)
                return 0f;

            return def.AnimalMeleeDmg() * def.AnimalMeleeHitChance() / def.AnimalMeleeCooldown();
        }

        public static float AnimalArmorPenetration(this ThingDef def)
        {
            if (def.tools == null)
                return 0f;

            float ArmorPenetration(Tool tool) => tool.armorPenetration < 0f ? tool.power * 0.015f : tool.armorPenetration;
            float Weight(Tool tool) => tool.power >= 0.001f ? tool.power * tool.power * tool.chanceFactor * 0.3f : 1f;

            float toolsWeigth = 0f;
            foreach (var tool in def.tools) toolsWeigth += Weight(tool) * tool.capacities.Count;
            if (toolsWeigth == 0f) return 0f;

            float ap = 0f;
            foreach (var tool in def.tools) ap += Weight(tool) * tool.capacities.Count / toolsWeigth * ArmorPenetration(tool);

            return ap;
        }

        public static float AnimalMeleeHitChance(this ThingDef def)
        {
            var kindDef = DefDatabase<PawnKindDef>.AllDefs.Where(k => k.defName == def.defName).FirstOrDefault();
            Pawn_AgeTracker_Patch.SkipNextPawnKindDef = kindDef;
            Pawn pawn = new Pawn { def = def, kindDef = kindDef };
            pawn.ageTracker = new Pawn_AgeTracker(pawn);
            pawn.health = new Pawn_HealthTracker(pawn);
            pawn.mindState = new Pawn_MindState(pawn);
            return pawn.GetStatValue(StatDefOf.MeleeHitChance);
        }

        public static float AnimalMeleeDmg(this ThingDef def)
        {
            float Weight(Tool tool) => tool.power >= 0.001f ? tool.power * tool.power * tool.chanceFactor * 0.3f : 1f;
            var tools = def.tools;
            float toolsWeigth = 0f;
            foreach (var tool in tools) toolsWeigth += Weight(tool) * tool.capacities.Count;
            if (toolsWeigth == 0f) return 0f;

            float dmg = 0f;
            foreach (var tool in tools) dmg += Weight(tool) * tool.capacities.Count / toolsWeigth * tool.power;

            return dmg;
        }

        public static float AnimalMeleeCooldown(this ThingDef def)
        {
            float Weight(Tool tool) => tool.power >= 0.001f ? tool.power * tool.power * tool.chanceFactor * 0.3f : 1f;

            var tools = def.tools;
            float toolsWeigth = 0f;
            foreach (var tool in tools) toolsWeigth += Weight(tool) * tool.capacities.Count;
            if (toolsWeigth == 0f) return 1f;

            float cooldown = 0f;
            foreach (var tool in tools) cooldown += Weight(tool) * tool.capacities.Count / toolsWeigth * tool.cooldownTime.SecondsToTicks();

            return cooldown / 60f;
        }
    }
}