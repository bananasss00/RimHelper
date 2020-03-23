using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using IPCInterface.Extensions;
using RimHelperProxyMod.Extensions;
using Verse;

namespace RimHelperProxyMod.Functions
{
    public static class PawnsHeddifs
    {
        public static string Get()
        {
            StringBuilder response = new StringBuilder();

            var ticksToDisappear = typeof(HediffComp_Disappears).GetField("ticksToDisappear", BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (Pawn th in Find.CurrentMap.listerThings.ThingsInGroup(ThingRequestGroup.Pawn))
            {
                if (th.Name == null)
                    continue;

                var lines = new List<string>();
                var hediffsDisappears = th.health?.hediffSet?.hediffs
                    .OfType<HediffWithComps>()
                    .Select(h => h.TryGetComp<HediffComp_Disappears>())
                    .Where(h => h != null);

                foreach (var hediff in hediffsDisappears.OrEmptyIfNull())
                {
                    int ticks = (int)ticksToDisappear.GetValue(hediff);
                    lines.Add($"   {hediff.Def.LabelCap} осталось {Math.Round(ticks / 60000f, 2)} дней");
                }

                if (lines.Count > 0)
                {
                    response.AppendLine($"Pawn: {th.Name.ToStringFull}");
                    foreach (var line in lines)
                    {
                        response.AppendLine(line);
                    }
                }
            }

            return response.ToString();
        }


        private static IEnumerable<Hediff_Injury> FindPermanentInjurys(Pawn pawn)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            for (int i = 0; i < hediffs.Count; i++)
            {
                Hediff_Injury hediff_Injury2 = hediffs[i] as Hediff_Injury;
                if (hediff_Injury2 != null && hediff_Injury2.Visible && hediff_Injury2.IsPermanent() && hediff_Injury2.def.everCurableByItem)
                {
                    //if (hediff_Injury == null || hediff_Injury2.Severity > hediff_Injury.Severity)
                    {
                        yield return hediff_Injury2;
                    }
                }
            }
        }
    }
}