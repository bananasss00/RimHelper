using System;
using Verse;

namespace RimHelperProxyMod.Functions
{
    public static class ActiveIncidents
    {
        public static string Get()
        {
            string response = string.Empty;
            var ticksGame = Find.TickManager.TicksGame;
            var conditions = Find.CurrentMap.gameConditionManager.ActiveConditions;
            foreach (var condition in conditions)
            {
                var days = Math.Round((condition.startTick + condition.Duration - ticksGame) / 60000f, 2);
                response += $"'{condition.LabelCap}' осталось: {days} дней\r\n";
            }

            return response;
        }
    }
}