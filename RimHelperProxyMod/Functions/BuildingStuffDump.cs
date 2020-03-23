using System.Collections.Generic;
using System.IO;
using System.Linq;
using RimWorld;
using Verse;
using IPCInterface.Extensions;
using RimHelperProxyMod.Extensions;

namespace RimHelperProxyMod.Functions
{
    public class BuildingStuffDump
    {
        private readonly Dictionary<string, string> _usedStatDefsFactors = new Dictionary<string, string>(); // Все статы defName = label
        private readonly Dictionary<string, string> _usedStatDefsOffsets = new Dictionary<string, string>(); // Все статы defName = label
        private readonly List<string> _csvFileFactors = new List<string>();
        private readonly List<string> _csvFileOffsets = new List<string>();
        private readonly List<string> _csvFileFactorsFromDef = new List<string>();
        private readonly List<string> _csvFileOffsetsFromDef = new List<string>();

        public void DumpStatDefs(string fileName)
        {
            var file = new List<string>();

            var fields = typeof(StatDefOf).GetFields();
            foreach (var field in fields)
            {
                StatDef statDef = (StatDef)field.GetValue(null);
                file.Add($"[ColorizeOrder()]");
                file.Add($"[DisplayName(\"{statDef.LabelCap}\")]");
                file.Add($"public float? {field.Name} {{ get; set; }}");
                file.Add("");
            }

            File.WriteAllLines(fileName, file.ToArray());
        }

        public void SaveUsedStatDefs(string fileName)
        {
            var file = new List<string>();

            file.Add($"// Stat factors");
            foreach (var stat in _usedStatDefsFactors)
            {
                file.Add($"[ColorizeOrder()]");
                file.Add($"[DisplayName(\"{stat.Value}\")]");
                file.Add($"public float? {stat.Key} {{ get; set; }}");
                file.Add("");
            }

            file.Add($"// Stat offsets");
            foreach (var stat in _usedStatDefsOffsets)
            {
                file.Add($"[ColorizeOrder()]");
                file.Add($"[DisplayName(\"{stat.Value}\")]");
                file.Add($"public float? {stat.Key} {{ get; set; }}");
                file.Add("");
            }

            File.WriteAllLines(fileName, file.ToArray());
        }

        void CheckAddStatDef(Thing thing, ThingDef stuff, StatModifier modifier, Dictionary<string, string> uniqueDic, List<string> csvFile)
        {
            if (modifier != null && !modifier.value.IsNull())
            {
                if (uniqueDic != null && !uniqueDic.ContainsKey(modifier.stat.defName))
                {
                    uniqueDic.Add(modifier.stat.defName, modifier.stat.LabelCap);
                }

                string thingName = thing.Label;
                string stuffName = stuff.label;
                string modName = modifier.stat.label;
                float modValue = modifier.value;

                csvFile.Add($"=\"{thingName}\";=\"{stuffName}\";=\"{modName}\";=\"{modValue}\"");
            }
        }

        void CheckAddStatDef(ThingDef thing, ThingDef stuff, StatModifier modifier, Dictionary<string, string> uniqueDic, List<string> csvFile)
        {
            if (modifier != null && !modifier.value.IsNull())
            {
                if (uniqueDic != null && !uniqueDic.ContainsKey(modifier.stat.defName))
                {
                    uniqueDic.Add(modifier.stat.defName, modifier.stat.LabelCap);
                }

                string thingName = thing.label;
                string stuffName = stuff.label;
                string modName = modifier.stat.label;
                float modValue = modifier.value;

                csvFile.Add($"=\"{thingName}\";=\"{stuffName}\";=\"{modName}\";=\"{modValue}\"");
            }
        }

        public BuildingStuffDump()
        {
            if (!Directory.Exists("_Dumps_"))
                Directory.CreateDirectory("_Dumps_");

            var path = @"_Dumps_\";
            //var path = @"";

            DumpStatDefs(path + "DumpStatDefs.cs");

            var allStuff = from d in DefDatabase<ThingDef>.AllDefs
                where d.IsStuff
                select d;
            var allMadeFromStuff = from d in DefDatabase<ThingDef>.AllDefs
                where d.MadeFromStuff
                select d;
            var allStatDefs = from s in typeof(StatDefOf).GetFields()
                where s.GetValue(null) != null
                select (StatDef)s.GetValue(null);

            foreach (ThingDef thingDef in allMadeFromStuff)
            {
                IEnumerable<ThingDef> avalaiableStuffs = allStuff.Where(s => s.stuffProps.CanMake(thingDef));
                foreach (ThingDef stuff in avalaiableStuffs.OrEmptyIfNull())
                {
                    Thing thing = ThingMaker.MakeThing(thingDef, stuff);
                    foreach (var statDef in allStatDefs)
                    {
                        StatModifier factor = thing.Stuff?.stuffProps?.statFactors?.FirstOrDefault(fa => fa.stat == statDef);
                        StatModifier offset = thing.Stuff?.stuffProps?.statOffsets?.FirstOrDefault(fa => fa.stat == statDef);
                        StatModifier factorFromDef = stuff.stuffProps?.statFactors?.FirstOrDefault(fa => fa.stat == statDef);
                        StatModifier offsetFromDef = stuff.stuffProps?.statOffsets?.FirstOrDefault(fa => fa.stat == statDef);
                        CheckAddStatDef(thing, stuff, factor, _usedStatDefsFactors, _csvFileFactors);
                        CheckAddStatDef(thing, stuff, offset, _usedStatDefsOffsets, _csvFileOffsets);
                        CheckAddStatDef(thingDef, stuff, factorFromDef, null, _csvFileFactorsFromDef);
                        CheckAddStatDef(thingDef, stuff, offsetFromDef, null, _csvFileOffsetsFromDef);
                    }
                    thing.Destroy();
                }
            }

            SaveUsedStatDefs(path + "DumpUsedStatDefs(Modifiers).cs");
            File.WriteAllLines(path + "DumpThingsFromStuff(Factors).csv", _csvFileFactors.ToArray()/*, Encoding.GetEncoding(1251)*/);
            File.WriteAllLines(path + "DumpThingsFromStuff(Offsets).csv", _csvFileOffsets.ToArray()/*, Encoding.GetEncoding(1251)*/);
            File.WriteAllLines(path + "DumpThingsFromStuff(FactorsFromDef).csv", _csvFileFactorsFromDef.ToArray()/*, Encoding.GetEncoding(1251)*/);
            File.WriteAllLines(path + "DumpThingsFromStuff(OffsetsFromDef).csv", _csvFileOffsetsFromDef.ToArray()/*, Encoding.GetEncoding(1251)*/);
        }
    }
}