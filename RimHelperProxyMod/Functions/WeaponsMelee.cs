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
    public static class WeaponsMelee
    {
        private const int RNG_TOUCH = 4;
        private const int RNG_SHORT = 15;
        private const int RNG_MEDIUM = 30;
        private const int RNG_LONG = 50;
        private const int TPS = 60;
        private static readonly FieldInfo _damageAmountBase = AccessTools.Field(typeof(ProjectileProperties), "damageAmountBase");

        public static List<WeaponMelee> Get()
        {
            if (CE_MeleeCounterParryBonus == null)
            {
                Type CE_StatDefOf = AccessTools.TypeByName("CombatExtended.CE_StatDefOf");
                if (CE_StatDefOf != null)
                {
                    var MeleeCounterParryBonus = AccessTools.Field(CE_StatDefOf, "MeleeCounterParryBonus");
                    if (MeleeCounterParryBonus != null)
                    {
                        CE_MeleeCounterParryBonus = MeleeCounterParryBonus.GetValue(null) as StatDef;
                    }
                }
            }

            var rows = new List<WeaponMelee>();

            var dataSources = from d in DefDatabase<ThingDef>.AllDefs
                where d.IsWeapon && d.IsMeleeWeapon/* && d.tradeability.TraderCanSell()*/
                orderby d.BaseMarketValue
                select d;

            foreach (var d in dataSources)
            {
                rows.Add(MakeRow(d));
            }

            return rows;
        }
        
        private static StatDef CE_MeleeCounterParryBonus = null;

        private static WeaponMelee MakeRow(ThingDef d)
        {
            var row = new WeaponMelee { Label = d.LabelCap, Description = d.DescriptionDetailed, MarketValue = d.BaseMarketValue, ItemsOnMap = d.CountOnMap()};
            
            try
            {
                row.TexturePath = d.modContentPack.RootDir + @"\Textures\" + d.graphicData.texPath;
            }
            catch
            {
            }

            try
            {
                float tmpCldwn = 1f;
                float tmpDmg = 0f;
                bool usethis = false;
                float cooldown = 0f, damage = 0f;
                string damageType = "";

                foreach (Tool tl in d.tools.OrEmptyIfNull())
                {
                    usethis = false;
                    if (tmpDmg / tmpCldwn < tl.power / tl.cooldownTime)
                    {
                        cooldown = tl.cooldownTime;
                        damage = tl.power;
                        usethis = true;
                    }
                    if (usethis)
                    {
                        foreach (ToolCapacityDef tcd in tl.capacities)
                        {
                            damageType = tcd.label + " (" + tl.label + ")";
                        }
                    }
                }

                // In HSK GetStatValue cause exception for MeleeWeapon_Shocker, MeleeWeapon_ElectricBaton
                row.CanCraft = d.CanCraft();
                row.Dps = (float)Math.Round(d.GetStatValueAbstract(StatDefOf.MeleeWeapon_AverageDPS), 2);
                row.Damage = damage;
                row.Cooldown = cooldown;
                row.TechLevel = d.techLevel.ToStringHuman().CapitalizeFirst();

                //public float? MeleeCritChance { get; set; } //Шанс критического удара
                //public float? MeleeDodgeChance { get; set; } //Шанс уворота в рукопашной
                //public float? MeleeParryChance { get; set; } //Шанс парировать в ближнем бою
                if (d.equippedStatOffsets != null)
                {
                    foreach (var statMod in d.equippedStatOffsets)
                    {
                        var rowProp = typeof(WeaponMelee).GetProperty(statMod.stat.defName);
                        rowProp?.SetValue(row, statMod.value.Nullify().ByStyle(statMod.stat.toStringStyle), null);
                    }
                }

                row.CE_OneHanded = d.weaponTags?.Contains("CE_OneHandedWeapon") ?? false;

                // CombatExtended
                if (CE_MeleeCounterParryBonus != null)
                {
                    row.CE_MeleeCounterParryBonus = CE_MeleeCounterParryBonus.Worker.GetValueAbstract(d, null);
                }

                if (d.weaponTags != null)
                    row.WeaponTags = string.Join("; ", d.weaponTags.OrderBy(x => x).ToArray());
            }
            catch (Exception e)
            {
                Log.Error($"{d.LabelCap} - {e.Message} - {e.StackTrace}");
            }

            return row;
        }

        private static float getDps(float damage, float cooldown)
        {
            return (float)Math.Round(damage / cooldown, 2);
        }
    }
}