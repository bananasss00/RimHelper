using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class Debuff : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [ColorizeOrder(ColorizeOrderOption.None)]
        [LocalizedDisplayName("Debuff_StackLimit")]
        public float? StackLimit { get; set; }

        [ColorizeOrder(ColorizeOrderOption.None)]
        [LocalizedDisplayName("Debuff_DurationDays")]
        public float? DurationDays { get; set; }

        [ColorizeOrder(ColorizeOrderOption.None)]
        [LocalizedDisplayName("Debuff_StackedEffectMultiplier")]
        public float? StackedEffectMultiplier { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Debuff_X1")]
        public float? X1 { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Debuff_X2")]
        public float? X2 { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Debuff_X3")]
        public float? X3 { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Debuff_X4")]
        public float? X4 { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Debuff_X5")]
        public float? X5 { get; set; }

        [LocalizedDisplayName("Debuff_XMore")]
        public bool XMore { get; set; }
    }
}