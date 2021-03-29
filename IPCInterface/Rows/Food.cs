using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class Food : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Food_Nutrition")]
        public float? Nutrition { get; set; }

        [LocalizedDisplayName("Food_JoyKind")]
        public string JoyKind { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Food_Joy")]
        public float? Joy { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Food_IsCorpse")]
        public bool IsCorpse { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Food_IsMeat")]
        public bool IsMeat { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Food_IsMeal")]
        public bool IsMeal { get; set; }

        [LocalizedDisplayName("Food_DrugCategory")]
        public string DrugCategory { get; set; }

        [LocalizedDisplayName("Food_FoodType")]
        public string FoodType { get; set; }

        [LocalizedDisplayName("Food_IngestEffect")]
        public string IngestEffect { get; set; }

        [LocalizedDisplayName("Food_Preferability")]
        public string Preferability { get; set; }



        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Drug_PainFactor")]
        public float? PainFactor { get; set; }

        [LocalizedDisplayName("Drug_SeverityDays")]
        public string SeverityDays { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_BaseMoodEffect")]
        public float? BaseMoodEffect { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_Consciousness")]
        public float? Consciousness { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_Moving")]
        public float? Moving { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_Manipulation")]
        public float? Manipulation { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_Talking")]
        public float? Talking { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_Eating")]
        public float? Eating { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_Sight")]
        public float? Sight { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_Hearing")]
        public float? Hearing { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_Breathing")]
        public float? Breathing { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_BloodFiltration")]
        public float? BloodFiltration { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_BloodPumping")]
        public float? BloodPumping { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_Metabolism")]
        public float? Metabolism { get; set; }
    }
}