using System;
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
    public static class WeaponsRanged
    {
        private const int RNG_TOUCH = 4;
        private const int RNG_SHORT = 15;
        private const int RNG_MEDIUM = 30;
        private const int RNG_LONG = 50;
        private const int TPS = 60;
        private static readonly FieldInfo _damageAmountBase = AccessTools.Field(typeof(ProjectileProperties), "damageAmountBase");

        public static bool CombatExtendedFound() => ModsConfig.ActiveModsInLoadOrder.Any(m => m.Name == "Combat Extended");

        public static List<WeaponRanged> Get()
        {
            var rows = new List<WeaponRanged>();

            var dataSources = from d in DefDatabase<ThingDef>.AllDefs
                //where d.IsWeapon && d.IsRangedWeapon && (d.tradeability.TraderCanSell() || (d.weaponTags != null && d.weaponTags.Contains("TurretGun")))
                where d.IsWeapon && d.IsRangedWeapon || (d.weaponTags != null && d.weaponTags.Contains("TurretGun")) // this show bows
                orderby d.BaseMarketValue
                select d;

            Type ammoUserType = AccessTools.TypeByName("CombatExtended.CompProperties_AmmoUser");
            bool hasCombatExtended = CombatExtendedFound();

            foreach (var d in dataSources)
            {
                rows.Add(MakeRow(d, hasCombatExtended, ammoUserType));
            }

            return rows;
        }
        

        private static WeaponRanged MakeRow(ThingDef d, bool hasCombatExtended, Type ammoUserType = null)
        {
            var row = new WeaponRanged { Label = d.LabelCap, Description = d.DescriptionDetailed, MarketValue = d.BaseMarketValue, ItemsOnMap = d.CountOnMap()};
            
            try
            {
                row.TexturePath = d.modContentPack.RootDir + @"\Textures\" + d.graphicData.texPath;
            }
            catch
            {
            }

            try
            {
                var accuracyTouch = d.GetStatValueAbstract(StatDefOf.AccuracyTouch).ToPercent();
                var accuracyShort = d.GetStatValueAbstract(StatDefOf.AccuracyShort).ToPercent();
                var accuracyMedium = d.GetStatValueAbstract(StatDefOf.AccuracyMedium).ToPercent();
                var accuracyLong = d.GetStatValueAbstract(StatDefOf.AccuracyLong).ToPercent();
                var cooldown = d.GetStatValueAbstract(StatDefOf.RangedWeapon_Cooldown);
                var mass = d.BaseMass;
                
                var verb = d.Verbs.OfType<VerbProperties>().FirstOrDefault();
                int damage = getDamageAmount(d, verb.defaultProjectile.projectile);
                int burstShotCount = verb.burstShotCount > 0 ? verb.burstShotCount : 1;
                int ticksBetweenBurstShots = verb.ticksBetweenBurstShots > 0 ? verb.ticksBetweenBurstShots : 10;
                float warmup = verb.warmupTime;
                float maxRange = verb.range;
                float minRange = verb.minRange;
                float burstShotFireRate = (float)Math.Round(60f / verb.ticksBetweenBurstShots.TicksToSeconds());

                row.CanCraft = d.CanCraft();
                row.Dps = getDps(damage, burstShotCount, cooldown, warmup, ticksBetweenBurstShots);
                row.Rpm = burstShotFireRate;
                row.Damage = damage;
                row.BurstShotCount = burstShotCount;
                row.MaxRange = maxRange;
                row.Cooldown = cooldown;
                row.WarmupTime = warmup;
                row.Accuracy = getAccuracyStr(minRange, maxRange, accuracyTouch, accuracyShort, accuracyMedium, accuracyLong);
                row.TechLevel = d.techLevel.ToStringHuman().CapitalizeFirst();
				
                if (hasCombatExtended)
                {
                    var ceAmmo = d.comps.FirstOrDefault(x => x.GetType() == ammoUserType); // d.GetCompProperties<CompProperties_AmmoUser>()
                    row.DamageType = ceAmmo != null ? Traverse.Create(ceAmmo).Field("ammoSet").Property("LabelCap").GetValue<string>() : verb.defaultProjectile.projectile.damageDef.label;
                    row.CE_SightsEfficiency = d.GetStatValueAbstract(StatDef.Named("SightsEfficiency")).ToPercent();
                    row.CE_ShotSpread = d.GetStatValueAbstract(StatDef.Named("ShotSpread"));
                    row.CE_SwayFactor = d.GetStatValueAbstract(StatDef.Named("SwayFactor"));
                    row.CE_OneHanded = d.weaponTags?.Contains("CE_OneHandedWeapon") ?? false;
                }
                else row.DamageType = verb.defaultProjectile.projectile.damageDef.label;

                if (d.weaponTags != null)
                    row.WeaponTags = string.Join("; ", d.weaponTags.ToArray());
            }
            catch (Exception e)
            {
                Log.Error($"{d.LabelCap} - {e.Message} - {e.StackTrace}");
            }

            return row;
        }

        private static float? getDps(float damage, int burstShotCount, float cooldown, float warmup, int ticksBetweenBurstShots)
        {
            float burstDamage = damage * burstShotCount;
            float warmupTicks = (cooldown + warmup) * TPS;
            float burstTicks = burstShotCount * ticksBetweenBurstShots;
            float totalTime = (warmupTicks + burstTicks) / TPS;

            return (float?)Math.Round(burstDamage / totalTime, 2);
        }

        private static int getDamageAmount(ThingDef weapon, ProjectileProperties pp)
        {
            var damageAmountBase = (int)_damageAmountBase.GetValue(pp);

            float? wdm = weapon.GetStatValueAbstract(StatDefOf.RangedWeapon_DamageMultiplier);
            float weaponDamageMultiplier = wdm == null ? 1f : (float)wdm;

            int num;
            if (damageAmountBase != -1)
            {
                num = damageAmountBase;
            }
            else
            {
                if (pp.damageDef == null)
                {
                    return 1;
                }
                num = pp.damageDef.defaultDamage;
            }

            return Mathf.RoundToInt((float)num * weaponDamageMultiplier);
        }

        private static string getAccuracyStr(float minRange, float maxRange, float accuracyTouch, float accuracyShort, float accuracyMedium, float accuracyLong)
        {
            StringBuilder sb = new StringBuilder();
            if (minRange > RNG_TOUCH || maxRange < RNG_TOUCH)
            {
                sb.Append(" - /");
            }
            else
            {
                sb.Append(" ").Append(Math.Round(accuracyTouch, 1).ToString()).Append(" /");
            }
            if (minRange > RNG_SHORT || maxRange < RNG_SHORT)
            {
                sb.Append(" - /");
            }
            else
            {
                sb.Append(" ").Append(Math.Round(accuracyShort, 1).ToString()).Append(" /");
            }
            if (minRange > RNG_MEDIUM || maxRange < RNG_MEDIUM)
            {
                sb.Append(" - /");
            }
            else
            {
                sb.Append(" ").Append(Math.Round(accuracyMedium, 1).ToString()).Append(" /");
            }
            if (minRange > RNG_LONG || maxRange < RNG_LONG)
            {
                sb.Append(" -");
            }
            else
            {
                sb.Append(" ").Append(Math.Round(accuracyLong, 1).ToString());
            }
            return sb.ToString();
        }
    }
}