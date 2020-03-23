using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class Drug : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [LocalizedDisplayName("Drug_DrugName")]
        public string DrugName { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Drug_Addictiveness")]
        public float? Addictiveness { get; set; }

        [LocalizedDisplayName("Drug_MinToleranceToAddict")]
        public string MinToleranceToAddict { get; set; }

        [LocalizedDisplayName("Drug_BaseSeverity")]
        public string BaseSeverity { get; set; }

        [LocalizedDisplayName("Drug_SeverityPerDay")]
        public string SeverityPerDay { get; set; }

        [LocalizedDisplayName("Drug_SeverityDays")]
        public string SeverityDays { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Drug_BaseMoodEffect")]
        public float? BaseMoodEffect { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Drug_PainFactor")]
        public float? PainFactor { get; set; }

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