using System;
using System.Collections.Generic;
using System.Linq;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;
using RimWorld;
using Verse;
using Backstory = IPCInterface.Rows.Backstory;

namespace RimHelperProxyMod.Functions
{
    public static class Backstorys
    {
        public static List<Backstory> Get()
        {
            var rows = new List<Backstory>();

            foreach (var bs in BackstoryDatabase.allBackstories.Values)
            {
                if (bs.identifier.StartsWith("Skynet_off"))
                    continue; // skip not useless bs

                rows.Add(MakeRow(bs));
            }

            return rows;
        }

        private static Backstory MakeRow(RimWorld.Backstory bs)
        {
            var row = new Backstory {Label = bs.title, Description = bs.baseDesc, BackstorySlot = bs.slot.ToString()};

            if (bs.spawnCategories?.Any() ?? false)
                row.spawnCategories = String.Join(", ", bs.spawnCategories.OrderBy(x => x).ToArray());

            if (bs.DisabledWorkGivers?.Any() ?? false)
                row.DisabledWorkGivers = String.Join(", ", bs.DisabledWorkGivers.Select(x => x.LabelCap).OrderBy(x => x).ToArray());

            if (bs.DisabledWorkTypes?.Any() ?? false)
                row.DisabledWorkTypes = String.Join(", ", bs.DisabledWorkTypes.Select(x => x.LabelCap).OrderBy(x => x).ToArray());

            if (bs.disallowedTraits?.Any() ?? false)
                row.disallowedTraits = String.Join(", ", bs.disallowedTraits.Select(x => x.def.defName).OrderBy(x => x).ToArray());

            if (bs.forcedTraits?.Any() ?? false)
                row.forcedTraits = String.Join(", ", bs.forcedTraits.Select(x => x.def.defName).OrderBy(x => x).ToArray());

            if (bs.workDisables != WorkTags.None)
                row.workDisables = bs.workDisables.ToString();

            if (bs.skillGainsResolved?.Any() ?? false)
            {
                foreach (var skillKv in bs.skillGainsResolved)
                {
                    var skillDef = skillKv.Key;
                    var rowProp = typeof(Backstory).GetProperty(skillDef.defName);
                    if (rowProp != null)
                    {
                        float? value = skillKv.Value;
                        rowProp.SetValue(row, value.Nullify(), null);
                    }
                    else
                    {
                        Log.Error($"[Backstory] Can't find skill row: {skillDef.defName}");
                    }
                }
            }

            return row;
        }
    }
}