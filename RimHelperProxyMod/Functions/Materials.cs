using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using RimWorld;
using Verse;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;

namespace RimHelperProxyMod.Functions
{
    public static class Materials
    {
        public static List<Material> Get()
        {
            if (CE_MeleePenetrationFactor == null)
            {
                Type CE_StatDefOf = AccessTools.TypeByName("CombatExtended.CE_StatDefOf");
                if (CE_StatDefOf != null)
                {
                    var MeleePenetrationFactor = AccessTools.Field(CE_StatDefOf, "MeleePenetrationFactor");
                    if (MeleePenetrationFactor != null)
                    {
                        CE_MeleePenetrationFactor = MeleePenetrationFactor.GetValue(null) as StatDef;
                    }
                }
            }

            var rows = new List<Material>();

            var dataSources = from d in DefDatabase<ThingDef>.AllDefs
                where d.IsStuff
                orderby d.BaseMarketValue
                select d;

            foreach (var d in dataSources)
            {
                rows.Add(MakeRow(d));
            }

            return rows;
        }

        private static StatDef CE_MeleePenetrationFactor = null;

        private static readonly Dictionary<string, bool> IsStatFactor = new Dictionary<string, bool>()
        {
            {"MeleeWeapon_CooldownMultiplier", true},
            //{"ConstructionSpeedFactor", true},
            {"DoorOpenSpeed", true},
            {"WorkToMake", true},
            {"ImmunityGainSpeedFactor", true},
            {"BedRestEffectiveness", true},
            {"WorkTableWorkSpeedFactor", true},
            {"ResearchSpeedFactor", true},
            //{"MaxHitPoints", true},
            //{"Flammability", true},
        };

        private static Material MakeRow(ThingDef d)
        {
            var row = new Material {Label = d.LabelCap, Description = d.DescriptionDetailed, ItemsOnMap = d.CountOnMap()};
            
            try
            {
                row.TexturePath = d.modContentPack.RootDir + @"\Textures\" + d.graphicData.texPath;
            }
            catch
            {
            }

            var fields = typeof(StatDefOf).GetFields();
            foreach (var field in fields)
            {
                var rowProp = typeof(Material).GetProperty(field.Name);
                if (rowProp != null)
                {
                    StatDef statDef = (StatDef)field.GetValue(null);

                    IsStatFactor.TryGetValue(field.Name, out bool isStatFactor);

                    float? value = isStatFactor ? d.GetStatFactorValue(statDef) : d.GetStatValueAbstract(statDef);
                    rowProp.SetValue(row, value.Nullify().ByStyle(statDef.toStringStyle), null);
                }
            }

            row.HitPoints = d.GetStatFactorValue(StatDefOf.MaxHitPoints).Nullify().ToPercent();

            // CombatExtended
            if (CE_MeleePenetrationFactor != null)
            {
                row.CE_MeleePenetrationFactor = d.GetStatFactorValue(CE_MeleePenetrationFactor);
            }

            return row;
        }
    }
}