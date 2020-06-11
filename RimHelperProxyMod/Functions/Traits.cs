using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;
using RimWorld;
using Verse;
using Trait = IPCInterface.Rows.Trait;

namespace RimHelperProxyMod.Functions
{
    public static class Traits
    {
        public static List<Trait> Get()
        {
            var rows = new List<Trait>();

            foreach (var td in Verse.DefDatabase<TraitDef>.AllDefsListForReading)
            {
                rows.AddRange(MakeRows(td));
            }

            return rows;
        }

        private static List<Trait> MakeRows(RimWorld.TraitDef td)
        {
            var result = new List<Trait>();

            string defName = td.defName, conflictingTraits = "", disabledWorkTypes = "", disabledWorkTags = "", requiredWorkTypes = "", requiredWorkTags = "";
                
            if (td.conflictingTraits?.Any() ?? false)
                conflictingTraits = String.Join(", ", td.conflictingTraits.Select(x => x.defName).OrderBy(x => x).ToArray());
            if (td.disabledWorkTypes?.Any() ?? false)
                disabledWorkTypes = String.Join(", ", td.disabledWorkTypes.Select(x => x.label).OrderBy(x => x).ToArray());
            if (td.disabledWorkTags != WorkTags.None)
                disabledWorkTags = td.disabledWorkTags.ToString();
            if (td.requiredWorkTypes?.Any() ?? false)
                requiredWorkTypes = String.Join(", ", td.requiredWorkTypes.Select(x => x.label).OrderBy(x => x).ToArray());
            if (td.requiredWorkTags != WorkTags.None)
                requiredWorkTags = td.requiredWorkTags.ToString();

            if (td.degreeDatas?.Any() ?? false)
            {
                foreach (var deg in td.degreeDatas)
                {
                    var row = new Trait
                    {
                        Label = Regex.Replace(deg.label, "<.*?>", ""), // remove html tags
                        Description = deg.description
                    };
                    row.defName = defName;
                    row.conflictingTraits = conflictingTraits;
                    row.disabledWorkTypes = disabledWorkTypes;
                    row.disabledWorkTags = disabledWorkTags;
                    row.requiredWorkTypes = requiredWorkTypes;
                    row.requiredWorkTags = requiredWorkTags;

                    if (deg.skillGains?.Any() ?? false)
                    {
                        foreach (var s in deg.skillGains)
                        {
                            var skillDef = s.Key;
                            var rowProp = typeof(Trait).GetProperty(skillDef.defName);
                            if (rowProp != null)
                            {
                                float? value = s.Value;
                                rowProp.SetValue(row, value.Nullify(), null);
                            }
                            else
                            {
                                Log.Error($"[Trait] Can't find skill row: {skillDef.defName}");
                            }
                        }
                    }

                    if (deg.statFactors?.Any() ?? false)
                    {
                        foreach (var s in deg.statFactors)
                        {
                            var rowProp = typeof(Trait).GetProperty(s.stat.defName);
                            if (rowProp != null)
                            {
                                float? value = s.value;
                                rowProp.SetValue(row, value.Nullify(), null);
                            }
                            else
                            {
                                Log.Error($"[Trait] Can't find statFactor row: {s.stat.defName}");
                            }
                        }
                    }

                    if (deg.statOffsets?.Any() ?? false)
                    {
                        foreach (var s in deg.statOffsets)
                        {
                            var rowProp = typeof(Trait).GetProperty(s.stat.defName);
                            if (rowProp != null)
                            {
                                float? value = s.value;
                                rowProp.SetValue(row, value.Nullify(), null);
                            }
                            else
                            {
                                Log.Error($"[Trait] Can't find statOffset row: {s.stat.defName}");
                            }
                        }
                    }

                    result.Add(row);
                }
            }

            return result;
        }
    }
}