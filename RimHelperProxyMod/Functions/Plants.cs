using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using RimWorld;
using Verse;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;
using Plant = IPCInterface.Rows.Plant;

namespace RimHelperProxyMod.Functions
{
    public static class Plants
    {
        public static List<Plant> Get()
        {
            var rows = new List<Plant>();
            var dataSources = from d in DefDatabase<ThingDef>.AllDefs
                where d.plant != null
                select d;

            foreach (var d in dataSources)
            {
                rows.Add(MakeRow(d));
            }

            return rows;
        }

        private static Plant MakeRow(ThingDef d)
        {
            var row = new Plant {Label = d.LabelCap, Description = d.DescriptionDetailed};

            float marketValue = d.plant.harvestedThingDef?.BaseMarketValue ?? 0f;
            float soilFertility = 1.8f;
            float growthSpeed180 = soilFertility * d.plant.fertilitySensitivity + (1 - d.plant.fertilitySensitivity);
            float growProgressIn10days = 10 * growthSpeed180;
            float mult = growProgressIn10days / d.plant.growDays;
            float growEff = mult * d.plant.harvestYield;
            float nutritionPer10Days = growEff * d.plant.harvestedThingDef?.ingestible?.CachedNutrition ?? 0f;
            float marketValuePer10Days = growEff * marketValue;

            row.NutritionPer10Days = nutritionPer10Days.Nullify().RoundTo2();
            row.MarketValuePer10Days = marketValuePer10Days.Nullify().RoundTo2();
            row.GrowDays = d.plant.growDays.Nullify().RoundTo2();
            row.GrowMinGlow = d.plant.growMinGlow.Nullify().ToPercent();
            row.FertilityMin = d.plant.fertilityMin.Nullify().ToPercent();
            row.FertilitySensitivity = d.plant.fertilitySensitivity.Nullify().ToPercent();
            row.HarvestYield = d.plant.harvestYield;
            row.LifespanDays = d.plant.LifespanDays.Nullify().RoundTo2();
            row.Sowable = d.plant.Sowable;
            row.IsTree = d.plant.IsTree;
            row.SowMinSkill = d.plant.sowMinSkill;
            row.HarvestedThingDef = d.plant.harvestedThingDef?.LabelCap;
            row.ProductNutrition = d.plant.harvestedThingDef?.ingestible?.CachedNutrition;
            row.ProductPreferability = d.plant.harvestedThingDef?.ingestible?.preferability.ToString();
            row.ProductDaysToRotStart = d.plant.harvestedThingDef?.GetCompProperties<CompProperties_Rottable>()?.daysToRotStart;
            row.ProductMarketValue = d.plant.harvestedThingDef?.BaseMarketValue;

            return row;
        }
    }
}