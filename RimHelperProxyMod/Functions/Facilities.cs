using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;

namespace RimHelperProxyMod.Functions
{
    public static class Facilities
    {
        public static List<Facility> Get()
        {
            var rows = new List<Facility>();

            var dataSources = from d in DefDatabase<ThingDef>.AllDefs
                where d.GetCompProperties<CompProperties_Facility>() != null
                select d;

            foreach (var d in dataSources)
            {
                rows.Add(MakeRow(d));
            }

            return rows;
        }

        public static string GetAvailableBuildings(ThingDef facility)
        {
            StringBuilder sb = new StringBuilder();

            var dataSources = from d in DefDatabase<ThingDef>.AllDefs
                where d.GetCompProperties<CompProperties_AffectedByFacilities>() != null
                select d;

            sb.AppendLine();

            foreach (var building in dataSources)
            {
                var prop = building.GetCompProperties<CompProperties_AffectedByFacilities>();
                if (prop.linkableFacilities != null && prop.linkableFacilities.Contains(facility))
                {
                    sb.AppendLine(building.LabelCap);
                }
            }

            return sb.ToString();
        }
        
        private static Facility MakeRow(ThingDef d)
        {
            var row = new Facility { Label = d.LabelCap, Description = d.DescriptionDetailed + GetAvailableBuildings(d) };

            var prop = d.GetCompProperties<CompProperties_Facility>();

            row.Max = prop.maxSimultaneous;

            if (prop.statOffsets != null && prop.statOffsets.Count > 0)
            {
                foreach (var stat in prop.statOffsets)
                {
                    var rowProp = typeof(Facility).GetProperty(stat.stat.defName);
                    if (rowProp != null)
                    {
                        rowProp.SetValue(row, stat.value.Nullify().ToPercent(), null);
                    }
                }
            }

            return row;
        }
    }
}