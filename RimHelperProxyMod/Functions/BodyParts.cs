using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using IPCInterface.Extensions;
using RimWorld;
using Verse;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;
using UnityEngine;

namespace RimHelperProxyMod.Functions
{
    public static class BodyParts
    {
        public static List<BodyPart> Get()
        {
            var rows = new List<BodyPart>();

            var dataSources = from d in DefDatabase<HediffDef>.AllDefs
                where d.addedPartProps != null || d.spawnThingOnRemoved != null
                select d;

            foreach (var d in dataSources)
            {
                rows.Add(MakeRow(d));
            }

            return rows;
        }
        

        private static BodyPart MakeRow(HediffDef d)
        {
            var row = new BodyPart { Label = d.LabelCap, Description = d.description };

            row.Efficiency = d.addedPartProps?.partEfficiency.ToPercent();

            if (d.stages != null)
            {
                var stage = d.stages.FirstOrDefault();

                // print statOffsets if exists
                var statOffsets = stage.statOffsets;
                if (statOffsets != null)
                {
                    row.Description += "\r\n";
                    row.Description += String.Join("\r\n", stage
                        .statOffsets
                        .Select(x => $" {x.stat.LabelCap} - {x.value}")
                        .ToArray());
                }
                
                foreach (var cap in stage.capMods)
                {
                    var rowProp = typeof(BodyPart).GetProperty(cap.capacity.defName);
                    if (rowProp != null)
                    {
                        rowProp.SetValue(row, cap.offset.Nullify().ToPercent(), null);
                    }
                }
            }

            return row;
        }
    }
}