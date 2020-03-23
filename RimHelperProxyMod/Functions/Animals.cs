using System.Collections.Generic;
using System.Linq;
using IPCInterface.Extensions;
using IPCInterface.Rows;
using RimWorld;
using Verse;
using RimHelperProxyMod.Extensions;

namespace RimHelperProxyMod.Functions
{
    public static class Animals
    {
        public static List<Animal> Get()
        {
            if (Current.ProgramState != ProgramState.Playing) // Create pawns/things not work from menu
                return new List<Animal> {new Animal {Label = "Available only on loaded map!" } };

            var rows = new List<Animal>();

            var dataSources = from k in DefDatabase<ThingDef>.AllDefs
                where k.race?.Animal ?? false
                select k;

            foreach (var d in dataSources.OrEmptyIfNull())
            {
                rows.Add(MakeRow(d));
            }

#if DEBUG
            rows.Dump("Dump.Animals.Rows.log");
#endif

            return rows;
        }

        private static Animal MakeRow(Verse.ThingDef d)
        {
            var row = new Animal {Label = d.LabelCap, Description = d.DescriptionDetailed};
            try
            {
                row.TexturePath = d.modContentPack.RootDir + @"\Textures\" + d.graphicData.texPath;
            }
            catch
            {
            }

            var fields = typeof(StatDefOf).GetFields();
            foreach (var field in fields)
            {
                var rowProp = typeof(Animal).GetProperty(field.Name);
                if (rowProp != null)
                {
                    var statDef = (StatDef) field.GetValue(null);

                    float? value = d.GetStatValueAbstract(statDef);
                    rowProp.SetValue(row, value.Nullify().ByStyle(statDef.toStringStyle), null);
                }
            }

            row.MeleeDPS_ = d.AnimalMeleeDps().Nullify().RoundTo2();
            row.MeleeArmorPenetration = d.AnimalArmorPenetration().ToPercent();
            row.ManhunterOnTameFailChance = d.race.manhunterOnTameFailChance.ToPercent();
            row.Predator = d.race.predator;
            row.Wildness = d.race.wildness.ToPercent();
            row.Petness = d.race.petness.ToPercent();
            row.PackAnimal = d.race.packAnimal;
            row.HerdAnimal = d.race.herdAnimal;
            row.Trainability = d.race.trainability?.LabelCap;

            var milkable = d.GetCompProperties<CompProperties_Milkable>();
            if (milkable != null)
            {
                row.MilkDef = milkable.milkDef.LabelCap;
                row.MilkIntervalDays = milkable.milkIntervalDays;
                row.MilkAmount = milkable.milkAmount;
            }

            var shearable = d.GetCompProperties<CompProperties_Shearable>();
            if (shearable != null)
            {
                row.WoolDef = shearable.woolDef.LabelCap;
                row.ShearIntervalDays = shearable.shearIntervalDays;
                row.WoolAmount = shearable.woolAmount;
            }

            var rescueDef = DefDatabase<TrainableDef>.AllDefs.FirstOrDefault(td => td.defName == "Rescue");
            var tr = d.race?.trainability?.intelligenceOrder;
            if (tr != null && rescueDef != null)
            {
                row.Train1 = tr >= TrainabilityDefOf.None.intelligenceOrder;
                row.Train2 = tr >= TrainabilityDefOf.Simple.intelligenceOrder;
                row.Train3 = tr >= TrainabilityDefOf.Intermediate.intelligenceOrder &&
                         d.race.baseBodySize >= rescueDef.minBodySize;
                row.Train4 = tr >= TrainabilityDefOf.Advanced.intelligenceOrder;
            }

            return row;
        }
    }
}