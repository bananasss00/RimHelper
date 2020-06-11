using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using Verse;
using IPCInterface.Rows;
using RimHelperProxyMod.Extensions;

namespace RimHelperProxyMod.Functions
{
    public static class CEAmmos
    {
        public static bool CombatExtendedFound() => ModsConfig.ActiveModsInLoadOrder.Any(m => m.Name == "Combat Extended");

        public static List<CEAmmo> Get()
        {
            var rows = new List<CEAmmo>();

            if (!CombatExtendedFound())
            {
                rows.Add(new CEAmmo {Label = "Combat Extended not found!"});
                return rows;
            }

            var ammoSetDefs = (IEnumerable)typeof(DefDatabase<>)
                .MakeGenericType(AccessTools.TypeByName("CombatExtended.AmmoSetDef"))
                .GetProperty("AllDefs", BindingFlags.Static | BindingFlags.Public)
                .GetGetMethod()
                .Invoke(null, null);

            if (ammoSetDefs == null)
            {
                rows.Add(new CEAmmo {Label = "Can't get AmmoSetDef's!"});
                return rows;
            }

            foreach (var ammoSetDef in ammoSetDefs)
            {
                rows.AddRange(MakeRow(ammoSetDef));
            }

            return rows;
        }
        

        private static List<CEAmmo> MakeRow(object ammoSetDef)
        {
            var rows = new List<CEAmmo>();

            foreach (var ammoType in Traverse.Create(ammoSetDef).Field("ammoTypes").GetValue<IEnumerable>())
            {
                // default fields
                var ammoLabelCap = Traverse.Create(ammoType).Field("ammo").Field("label").GetValue<string>();
                var ammoDescriptionDetailed = Traverse.Create(ammoType).Field("ammo").Property("DescriptionDetailed").GetValue<string>();

                // ammo users
                string users = String.Join(Environment.NewLine,
                    Traverse.Create(ammoType).Field("ammo").Property("Users").GetValue< List<ThingDef> >()
                    .Select(x => x.label)
                    .ToArray()
                );
                ammoDescriptionDetailed += $"{Environment.NewLine}{users}";

                // ammo stats
                var projectile = Traverse.Create(ammoType).Field("projectile").Field("projectile").GetValue<object>();
                var damageDef = Traverse.Create(projectile).Field("damageDef").GetValue<DamageDef>();
                var damageAmountBase = Traverse.Create(projectile).Field("damageAmountBase").GetValue<int>();
				var armorPenetrationBase = Traverse.Create(projectile).Field("armorPenetrationBase").GetValue<float>(); // OUTDATED
                var armorPenetrationSharp = Traverse.Create(projectile).Field("armorPenetrationSharp").GetValue<float>();
                var armorPenetrationBlunt = Traverse.Create(projectile).Field("armorPenetrationBlunt").GetValue<float>();
                var speed = Traverse.Create(projectile).Field("speed").GetValue<float>();

                string damageType2 = null;
                float? damage2 = null;
                IEnumerable secondaryDamage = Traverse.Create(projectile).Field("secondaryDamage").GetValue<IEnumerable>();
                if (secondaryDamage != null)
                {
                    var it = secondaryDamage.GetEnumerator();
                    if (it.MoveNext())
                    {
                        var first = it.Current;
                        var dmgDef = Traverse.Create(first).Field("def").GetValue<DamageDef>();
                        var amount = Traverse.Create(first).Field("amount").GetValue<int>();
                        
                        damageType2 = dmgDef.LabelCap;
                        if (amount != 0)
                            damage2 = amount;
                    }
                }
                // append ammo
                rows.Add(new CEAmmo
                {
                    Label = ammoLabelCap, 
                    DamageType = damageDef.LabelCap,
                    Description = ammoDescriptionDetailed,
                    Damage = damageAmountBase,
					ArmorPenetration = armorPenetrationBase.Nullify(), // OUTDATED
                    ArmorPenetrationSharp = armorPenetrationSharp.Nullify(),
                    ArmorPenetrationBlunt = armorPenetrationBlunt.Nullify(),
                    Speed = speed,
                    DamageType2 = damageType2,
                    Damage2 = damage2
                });
            }

            return rows;
        }
    }
}