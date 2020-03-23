﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using IPCInterface;
using IPCInterface.Extensions;
using RimWorld;
using Verse;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;
using UnityEngine;
using Apparel = IPCInterface.Rows.Apparel;

namespace RimHelperProxyMod.Functions
{
    public static class Apparels
    {
        public static List<IPCInterface.Rows.Apparel> Get()
        {
            var rows = new List<IPCInterface.Rows.Apparel>();

            var dataSources = from d in DefDatabase<ThingDef>.AllDefs
                where d.IsApparel
                orderby d.BaseMarketValue
                select d;

            foreach (var d in dataSources)
            {
                if (d.MadeFromStuff)
                {
                    ThingDef defStuff = GenStuff.DefaultStuffFor(d);
                    foreach (var stuff in GenStuff.AllowedStuffsFor(d))
                    {
                        rows.Add(MakeRow(d, defStuff, stuff));
                    }
                }
                else
                {
                    rows.Add(MakeRow(d));
                }
            }

            return rows;
        }
        

        private static Apparel MakeRow(ThingDef d, ThingDef defStuff = null, ThingDef stuff = null)
        {
            var row = new Apparel { Label = d.LabelCap, Description = d.DescriptionDetailed, MarketValue_ = d.BaseMarketValue, ItemsOnMap = d.CountOnMap()};

            row.CanCraft = d.CanCraft(); //d.recipeMaker != null;
            row.DefMaterial = defStuff?.label;

            try
            {
                row.TexturePath = d.modContentPack.RootDir + @"\Textures\" + d.graphicData.texPath;
            }
            catch
            {
            }

            try
            {
                //var defStuff = d.MadeFromStuff ? GenStuff.DefaultStuffFor(d) : null;
                if (d.MadeFromStuff)
                    defStuff = stuff ?? GenStuff.DefaultStuffFor(d);

                if (defStuff != null)
                    row.Label += $"({defStuff.label})";

                row.Material = defStuff?.label;
                row.CanCraft = d.CanCraft();
                row.Body = String.Join(",", d.apparel.bodyPartGroups.Select(x => x.LabelCap).ToArray());
                row.Layer = String.Join(",", d.apparel.layers.Select(x => x.LabelCap).ToArray());
                row.ArmorBlunt = d.GetStatValueAbstract(StatDefOf.ArmorRating_Blunt, defStuff).Nullify().ToPercent();
                row.ArmorSharp = d.GetStatValueAbstract(StatDefOf.ArmorRating_Sharp, defStuff).Nullify().ToPercent();
                row.ArmorHeat = d.GetStatValueAbstract(StatDefOf.ArmorRating_Heat, defStuff).Nullify().ToPercent();
                row.InsulationCold = d.GetStatValueAbstract(StatDefOf.Insulation_Cold, defStuff).Nullify().RoundTo2();
                row.InsulationHeat = d.GetStatValueAbstract(StatDefOf.Insulation_Heat, defStuff).Nullify().RoundTo2();

                // AutoStatdefs
                foreach (var offset in d.equippedStatOffsets)
                {
                    var prop = typeof(Apparel).GetProperty(offset.stat.defName);
                    if (prop != null)
                    {
                        prop.SetValue(row, offset.value.Nullify().ByStyle(offset.stat.toStringStyle), null);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"{d.LabelCap} - {e.Message} - {e.StackTrace}");
            }

            return row;
        }
    }
}