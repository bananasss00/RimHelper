using System.Collections.Generic;
using System.Linq;
using IPCInterface.Extensions;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;
using RimWorld;
using Verse;

namespace RimHelperProxyMod.Functions
{
    public static class Drugs
    {
        public static List<Drug> Get()
        {
            var rows = new List<Drug>();
            var drugs = from d in DefDatabase<Verse.ThingDef>.AllDefs where d.IsDrug select d;

            foreach (var d in drugs)
            {
                rows.Add(MakeRow(d));
                var doers = GetValidNarcoDoers(d);
                if (doers == null)
                    continue;

                // get BEFORE tolerance heddif for detect he in 'doers' iterate
                var toleranceHeddif = GetToleranceDef(doers);
#if DEBUG
                Log.Warning($"def: {d.LabelCap}, GetValidNarcoDoers = {doers.ToList().Count}");
#endif

                IngestionOutcomeDoer_GiveHediff doerForAddiction = null;
                foreach (var doer in doers.OrEmptyIfNull())
                {
                    bool isToleranceHeddif = toleranceHeddif != null && doer.hediffDef == toleranceHeddif;
                    if (isToleranceHeddif)
                    {
                        rows.Add(GetFromHeddifTolerance(d, doer.hediffDef, doer.severity,
                            d.GetCompProperties<CompProperties_Drug>()?.minToleranceToAddict));
                    }
                    else
                    {
                        var thought = DefDatabase<RimWorld.ThoughtDef>.AllDefs.Where(t => t.hediff == doer.hediffDef)
                            .FirstOrDefault();
                        rows.AddRange(GetFromHeddifStages(d, doer.hediffDef, doer.severity, thought));
                        doerForAddiction = doer;
                    }
                }

                //get addictionHeddif
                var addictionHeddif = GetAddictionDef(doers);
                if (addictionHeddif != null)
                {
                    var addictionThought = DefDatabase<RimWorld.ThoughtDef>.AllDefs.Where(t => t.hediff == addictionHeddif).FirstOrDefault();
                    rows.Add(GetFromHeddifAddiction(d, addictionHeddif, doerForAddiction?.hediffDef.initialSeverity ?? 0f, addictionThought));
                }
            }

            return rows;
        }

        private static Drug MakeRow(ThingDef d)
        {
            var row = new Drug {Label = d.LabelCap, DrugName = d.LabelCap, Description = d.DescriptionDetailed};
            var compDrug = d.GetCompProperties<CompProperties_Drug>();
            row.Addictiveness = compDrug?.addictiveness.ToPercent();
            row.BaseSeverity = "+" + d.ingestible?.outcomeDoers?.OfType<IngestionOutcomeDoer_GiveHediff>().FirstOrDefault()?.severity.ToString("F2");

            return row;
        }

        private static Drug GetFromHeddifTolerance(ThingDef def, HediffDef hediff, float doerSeverity, float? minToleranceToAddict = null)
        {
            var row = new Drug {DrugName = def.LabelCap};

            var severityPerDay = hediff.CompProps<HediffCompProperties_SeverityPerDay>()?.severityPerDay;

            if (minToleranceToAddict == null)
                Log.Error($"[AddRowFromHeddifTolerance] minToleranceToAddict = null");

            var severity = CalcSeverityLevelByMin(doerSeverity, (float)minToleranceToAddict, out int count);

            row.Label = $"   {hediff.label}";
            row.Description = $"Минимальная толерантность для привыкания: {minToleranceToAddict}\r\nЗа приём добавляется толерантности: {doerSeverity}\r\nШанс привыкания появится после употребленных доз: x{count}({severity})";
            row.BaseSeverity = "+" + doerSeverity.ToString("F2");

            if (minToleranceToAddict != null)
                row.MinToleranceToAddict = $"{((float)minToleranceToAddict).ToString("F2")}(x{count})";

            if (severityPerDay != null)
            {
                row.SeverityPerDay = ((float)severityPerDay).ToString("F2");
                row.SeverityDays = GetSeverityDays(severity, (float)severityPerDay).ToString("F2");
            }

            return row;
        }

        private static Drug GetFromHeddifAddiction(ThingDef def, HediffDef hediff, float doerSeverity, ThoughtDef thought)
        {
            var row = new Drug { DrugName = def.LabelCap };

            var hasThought = thought != null;
            var severityPerDay = hediff.CompProps<HediffCompProperties_SeverityPerDay>()?.severityPerDay;

            if (hediff.stages.Count > 1)
            {
                var stage = hediff.stages[1];
                row.BaseSeverity = doerSeverity.ToString("F2");
                row.Label = $"   Ломка";
                //row.Description = $"Это единственное состояние";
                if (hasThought)
                {
                    var thoughtStages = thought.stages;
                    row.BaseMoodEffect = 1 < thoughtStages.Count ? thoughtStages[1].baseMoodEffect : thoughtStages.Last().baseMoodEffect;
                }

                if (severityPerDay != null)
                {
                    row.SeverityPerDay = ((float)severityPerDay).ToString("F2");
                    row.SeverityDays = GetSeverityDays(doerSeverity, (float)severityPerDay).ToString("F2");
                }

                row.PainFactor = stage.painFactor.ToPercent();

                FillCapMods(ref row, hediff.stages[0]);
            }
            else Log.Error($"[GetFromHeddifAddiction] hediff.stages.Count not standart {hediff.stages.Count}");

            return row;
        }

        private static List<Drug> GetFromHeddifStages(ThingDef def, HediffDef hediff, float doerSeverity, RimWorld.ThoughtDef thought)
        {
            var result = new List<Drug>();
            var hasThought = thought != null;
            var severityPerDay = hediff.CompProps<HediffCompProperties_SeverityPerDay>()?.severityPerDay;

            for (int i = 0; i < hediff.stages.Count; ++i)
            {
                var stage = hediff.stages[i];

                var row = new Drug { DrugName = def.LabelCap };

                float severity;
                if (hediff.stages.Count == 1)
                {
                    severity = doerSeverity;
                    row.BaseSeverity = severity.ToString("F2");
                    row.Label = $"   {(string.IsNullOrEmpty(stage.label) ? hediff.label : stage.label)}(> {stage.minSeverity})";
                    row.Description = $"Это единственное состояние";
                }
                else
                {
                    int count = 1;
                    severity = stage.minSeverity == 0f ? doerSeverity : CalcSeverityLevelByMin(doerSeverity, stage.minSeverity, out count);
                    row.BaseSeverity = severity.ToString("F2");
                    row.Label = $"   {(string.IsNullOrEmpty(stage.label) ? hediff.label : stage.label)}(> {stage.minSeverity}) x{count}";
                    row.Description = $"Это состояние начинается после: {stage.minSeverity}\r\nДля этого состояния нужно принять раз: x{count}";
                }

                if (hasThought)
                {
                    var thoughtStages = thought.stages;
                    row.BaseMoodEffect = i < thoughtStages.Count ? thoughtStages[i].baseMoodEffect : thoughtStages.Last().baseMoodEffect;
                }

                if (severityPerDay != null)
                {
                    row.SeverityPerDay = ((float)severityPerDay).ToString("F2");
                    row.SeverityDays = GetSeverityDays(severity, (float)severityPerDay).ToString("F2");
                }

                row.PainFactor = stage.painFactor.ToPercent();

                FillCapMods(ref row, /*hediff.stages[0]*/stage); // why was [0] wtf?

                result.Add(row);
            }

            return result;
        }

        private static HediffDef GetAddictionDef(IEnumerable<IngestionOutcomeDoer_GiveHediff> doers)
        {
            return doers.OrEmptyIfNull().Select(doer => doer.toleranceChemical?.addictionHediff).FirstOrDefault();
        }

        private static HediffDef GetToleranceDef(IEnumerable<IngestionOutcomeDoer_GiveHediff> doers)
        {
            return doers.OrEmptyIfNull().Select(doer => doer.toleranceChemical?.toleranceHediff).FirstOrDefault();
        }

        private static IEnumerable<IngestionOutcomeDoer_GiveHediff> GetValidNarcoDoers(Verse.ThingDef def)
        {
            return def.ingestible?.outcomeDoers?.OfType<IngestionOutcomeDoer_GiveHediff>().Where(d => d.hediffDef?.stages != null);
        }

        private static float GetSeverityDays(float severity, float severityPerDay)
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

        private static float CalcSeverityLevelByMin(float severity, float minSeverity, out int count)
        {
            count = 0;
            float result = 0;
            while (result < minSeverity)
            {
                result += severity;
                count++;
            }

            return result;
        }

        private static void FillCapMods(ref Drug d, HediffStage stage)
        {
            var capMods = stage.capMods;
            if (capMods != null)
            {
                foreach (var modifier in capMods)
                {
                    var mod = typeof(Drug).GetProperty(modifier.capacity.defName);
                    if (mod != null)
                    {
                        mod.SetValue(d, modifier.offset.ToPercent(), null);
                    }
                    else Log.Error($"[FillCapMods] Unknown capMod '{modifier.capacity.defName}'");
                }
            }
        }

        private static void DumpDrugHeddif(HediffDef hediff)
        {
            if (hediff.stages != null)
            {
                Log.Error($"hediff {hediff.defName} - {hediff.LabelCap}, stages: {hediff.stages.Count}");
                foreach (var stage in hediff.stages)
                {
                    Log.Warning($"  hediff stage {stage.untranslatedLabel} - {stage.label}");
                }

                var thoughts = DefDatabase<RimWorld.ThoughtDef>.AllDefs.Where(t =>
                    t.hediff == hediff).ToList();
                Log.Error($"  ====THOUGTHS: {thoughts.Count}====");
                foreach (var th in thoughts)
                {
                    Log.Warning($"  thought {th.defName} - {th.LabelCap}, stages: {th.stages.Count}");
                    foreach (var stage in th.stages)
                    {
                        Log.Warning($"    thought stage {stage.untranslatedLabel} - {stage.label}");
                    }
                }
            }
        }

        private static void DumpCapMods(HediffDef hediff)
        {
            if (hediff.stages == null)
                return;

            Log.Error($"{hediff.defName} = {hediff.label}");
            foreach (var stage in hediff.stages)
            {
                Log.Error($"  {stage.untranslatedLabel} - {stage.label}");
                foreach (var mod in stage.capMods)
                {
                    Log.Warning($"    {mod.capacity.defName} - {mod.capacity.LabelCap}");
                }
            }
        }
    }
}