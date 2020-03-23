using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using RimWorld;
using Verse;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;
using UnityEngine;

namespace RimHelperProxyMod.Functions
{
    public static class Tools
    {
        public static List<ST_Tool> Get()
        {
            var rows = new List<ST_Tool>();

            var dataSources = from d in DefDatabase<ThingDef>.AllDefs
                where d.thingCategories != null && d.thingCategories.Exists(x => x.defName.StartsWith("SurvivalTools"))
                      || d.modExtensions != null && (
                          d.modExtensions.Find(x => x.ToString().StartsWith("SurvivalTools.SurvivalTool")) != null
                          || d.modExtensions.Find(x => x.ToString().StartsWith("RightToolForJob")) != null
                          )
                select d;

            foreach (var d in dataSources)
            {
                rows.Add(MakeRow(d));
            }

            return rows;
        }
        

        private static ST_Tool MakeRow(ThingDef d)
        {
            var row = new ST_Tool { Label = d.LabelCap, Description = d.DescriptionDetailed };
            
            List<StatModifier> GetSTBaseWorkStatFactors(DefModExtension ext) =>
                Traverse.Create(ext).Field<List<StatModifier>>("baseWorkStatFactors").Value;

            // Select SurvivalTool statModifiers
            var statModifiers = d.modExtensions != null ? d.modExtensions
                .Where(x => x.ToString().Contains("SurvivalToolProperties"))
                .Where(x => GetSTBaseWorkStatFactors(x) != null)
                .SelectMany(x => GetSTBaseWorkStatFactors(x)) : new List<StatModifier>();

            // Select equipped statModifiers
            if (d.equippedStatOffsets != null)
                statModifiers = statModifiers.Concat(d.equippedStatOffsets);

            foreach (var statMod in statModifiers)
            {
                var rowProp = typeof(ST_Tool).GetProperty(statMod.stat.defName);
                if (rowProp != null)
                {
                    rowProp.SetValue(row, statMod.value.Nullify().ByStyle(statMod.stat.toStringStyle), null);
                }
                else
                {
                    Log.Warning($"[RHPM-Tool] Stat '{statMod.stat.defName}' not defined");
                }
            }

            return row;
        }
    }
}