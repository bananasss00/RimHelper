using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Harmony;
using IPCInterface.Extensions;
using RimHelperProxyMod.Extensions;
using RimWorld;
using UnityEngine;
using Verse;


namespace RimHelperProxyMod.Functions
{
    public static class WeaponApparelDump
    {
        public static string Get()
        {
            ObjectDumper.ThrowExceptions = false;
            ObjectDumper.IncludePrivateMembers = true;
            ObjectDumper.ReplaceNewLineSymbol = true;

            if (!Directory.Exists("_Dumps_"))
                Directory.CreateDirectory("_Dumps_");

            void dump(object obj, string defName, string category, int level) {
                File.WriteAllText($"_Dumps_\\{category}-{defName}-{level}.txt", obj.Dump(level));
            }

            foreach (ThingDef d in DefDatabase<ThingDef>.AllDefsListForReading)
            {
                if (d.IsWeapon && (d.tradeability.TraderCanSell() || (d.weaponTags != null && d.weaponTags.Contains("TurretGun"))))
                {
                    dump(d, d.defName, "weapon", 0);
                    dump(d, d.defName, "weapon", 1);
                }
                else if (d.IsApparel)
                {
                    dump(d, d.defName, "apparel", 0);
                    dump(d, d.defName, "apparel", 1);
                }
                //else if (d is AmmoDef)
                //{
                //    var ammoDef = d as AmmoDef;
                //    dump(ammoDef, ammoDef.defName, "ammo", 0);
                //    dump(ammoDef, ammoDef.defName, "ammo", 1);
                //}
            }

            return "Finished";
        }
    }
}