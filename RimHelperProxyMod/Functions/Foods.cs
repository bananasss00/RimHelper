using System;
using System.Collections.Generic;
using System.Linq;
using IPCInterface.Extensions;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;
using RimWorld;
using Verse;

namespace RimHelperProxyMod.Functions
{
    public static class Foods
    {
        public static List<Food> Get()
        {
            var rows = new List<Food>();
            var foods = from d in DefDatabase<Verse.ThingDef>.AllDefs where d.IsIngestible select d;

            foreach (var d in foods)
            {
                var row = new Food {Label = d.LabelCap, Description = d.DescriptionDetailed};
                var ip = d.ingestible;

                row.Nutrition = ip.CachedNutrition.RoundTo2();
                row.JoyKind = ip.JoyKind?.label;
                row.Joy = ip.joy;
                row.IsCorpse = d.IsCorpse;
                row.IsMeat = d.IsMeat;
                row.IsMeal = ip.IsMeal;
                row.DrugCategory = ip.drugCategory.ToString();
                row.FoodType = ip.foodType.ToString();
                row.IngestEffect = ip.ingestEffect?.LabelCap;
                row.Preferability = ip.preferability.ToString();

                var doers = DoersGiveHediff(ip.outcomeDoers);
                if (doers != null)
                {
                    row.Description += $"\nHediffs:\n";
                    var toleranceDef = ToleranceDef(doers);
                    foreach (var doer in doers)
                    {
                        var hediff = doer.hediffDef;
                        if (hediff == null || hediff == toleranceDef || hediff.stages == null)
                            continue;

                        HediffStage stage = null;
                        int i = 0;
                        for (; i < hediff.stages.Count; i++)
                        {
                            if (doer.severity > hediff.stages[i].minSeverity)
                            {
                                stage = hediff.stages[i];
                                break;
                            }
                        }
                        if (stage == null)
                            continue; // недостаточно 1го употребления

                        row.Description += $"{hediff.LabelCap}({stage.label})\n";
                        row.PainFactor = stage.painFactor.ToPercent();

                        var severityPerDay = hediff.CompProps<HediffCompProperties_SeverityPerDay>()?.severityPerDay;
                        if (severityPerDay != null)
                            row.SeverityDays = SeverityDays(doer.severity, (float)severityPerDay).ToString("F2");

                        var thought = DefDatabase<ThoughtDef>.AllDefsListForReading.FirstOrDefault(t => t.hediff == doer.hediffDef);
                        if (thought != null && thought.stages != null)
                        {
                            row.BaseMoodEffect = i < thought.stages.Count
                                    ? thought.stages[i].baseMoodEffect
                                    : thought.stages.Last().baseMoodEffect;
                        }

                        FillCapMods(ref row, /*hediff.stages[0]*/stage);
                    }
                }
                rows.Add(row);
            }

            return rows;
        }

        static IEnumerable<IngestionOutcomeDoer_GiveHediff> DoersGiveHediff(IEnumerable<IngestionOutcomeDoer> d) => d?.OfType<IngestionOutcomeDoer_GiveHediff>()/*.Where(d => d.hediffDef?.stages != null)*/;

        static HediffDef ToleranceDef(IEnumerable<IngestionOutcomeDoer_GiveHediff> doers) => doers?.Select(doer => doer.toleranceChemical?.toleranceHediff).FirstOrDefault();

        static float SeverityDays(float severity, float severityPerDay)
        {
            //float severity = this.averageSeverityPerDayBeforeGeneration * daysPassed * Rand.Range(0.5f, 1.4f) + hediff.def.initialSeverity;
            if (severityPerDay >= 0)
                return float.PositiveInfinity;

            float days = 0;
            while (true)
            {
                days += 0.1f;
                if (severity + days * severityPerDay <= 0)
                    break;
            }

            return days;
        }

        private static void FillCapMods(ref Food d, HediffStage stage)
        {
            var capMods = stage.capMods;
            if (capMods != null)
            {
                foreach (var modifier in capMods)
                {
                    var mod = typeof(Food).GetProperty(modifier.capacity.defName);
                    if (mod != null)
                    {
                        mod.SetValue(d, modifier.offset.ToPercent(), null);
                    }
                    else Log.Error($"[FillCapMods] Unknown capMod '{modifier.capacity.defName}'");
                }
            }
        }
    }
}