using System;
using System.Collections.Generic;
using IPCInterface.Rows;
using RimWorld;
using Verse;

namespace RimHelperProxyMod.Functions
{
    public static class Debuffs
    {
        public static List<Debuff> Get()
        {
            var rows = new List<Debuff>();
            foreach (var t in DefDatabase<RimWorld.ThoughtDef>.AllDefs)
            {
                for (int i = 0; i < t.stages.Count; ++i)
                {
                    Debuff def = MakeRow(t, i);
                    if (def != null)
                        rows.Add(def);
                }
            }

            return rows;
        }

        private static float MoodOffsetOfGroup(float baseMood, float kef, int count)
        {
            float num1 = 0.0f;
            float num2 = 1f;
            float num3 = 0.0f;
            for (int index = 0; index < count; ++index)
            {
                num1 += baseMood;
                num3 += num2;
                num2 *= kef;
            }
            float num4 = num1 / (float)count;
            return (float)Math.Round(num4 * num3, 0);
        }

        private static Debuff MakeRow(ThoughtDef t, int stage)
        {
            if (t.stages[stage] == null || String.IsNullOrEmpty(t.stages[stage].label))
                return null;

            var row = new Debuff();
            row.Label = t.stages[stage].label;
            row.Description = t.stages[stage].description;
            row.StackLimit = t.stackLimit;
            row.DurationDays = t.durationDays;
            row.StackedEffectMultiplier = t.stackedEffectMultiplier;

            var max = row.StackLimit < 5 ? row.StackLimit : 5;
            for (int i = 1; i <= max; ++i)
            {
                var xN = typeof(Debuff).GetProperty("X" + i);
                xN.SetValue(row, MoodOffsetOfGroup(t.stages[stage].baseMoodEffect, (float)row.StackedEffectMultiplier, i), null);
            }

            row.XMore = row.StackLimit > 5;

            return row;
        }
    }
}