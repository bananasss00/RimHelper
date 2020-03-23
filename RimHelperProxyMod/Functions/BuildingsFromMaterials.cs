using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using IPCInterface.Extensions;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;

namespace RimHelperProxyMod.Functions
{
    public static class BuildingsFromMaterials
    {
        public static List<BuildingsFromMaterial> Get()
        {
            var rows = new List<BuildingsFromMaterial>();

            var allStuff = from d in DefDatabase<ThingDef>.AllDefs
                where d.IsStuff
                select d;

            var buildings = from d in DefDatabase<ThingDef>.AllDefs
                where d.MadeFromStuff && (d.IsBed || d.IsWorkTable || d.thingClass == typeof(Building_ResearchBench))
                select d;

            foreach (var building in buildings)
            {
                foreach (var stuff in allStuff.Where(s => s.stuffProps.CanMake(building)).OrEmptyIfNull())
                {
                    rows.Add(MakeRow(building, stuff));
                }
            }

            return rows;
        }

        private static BuildingsFromMaterial MakeRow(ThingDef building, ThingDef stuff)
        {
            var row = new BuildingsFromMaterial
                {Label = building.LabelCap, Material = stuff.LabelCap, Description = building.DescriptionDetailed};

            try
            {
                row.TexturePath = building.modContentPack.RootDir + @"\Textures\" + building.graphicData.texPath;
            }
            catch
            {
            }

            row.ImmunityGainSpeedFactor = stuff.GetStatFactorValue(StatDefOf.ImmunityGainSpeedFactor).ToPercent();
            row.BedRestEffectiveness = stuff.GetStatFactorValue(StatDefOf.BedRestEffectiveness).ToPercent();
            row.WorkTableWorkSpeedFactor = stuff.GetStatFactorValue(StatDefOf.WorkTableWorkSpeedFactor).ToPercent();
            row.ResearchSpeedFactor = stuff.GetStatFactorValue(StatDefOf.ResearchSpeedFactor).ToPercent();
            row.MaxHitPoints = stuff.GetStatFactorValue(StatDefOf.MaxHitPoints).ToPercent();
            row.MarketValue = stuff.GetStatFactorValue(StatDefOf.MarketValue).ToPercent();

            return row;
        }
    }
}