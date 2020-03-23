using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class BuildingsFromMaterial : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [LocalizedDisplayName("BuildingsFromMaterial_Material")]
        public string Material { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BuildingsFromMaterial_ImmunityGainSpeedFactor")]
        public float? ImmunityGainSpeedFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BuildingsFromMaterial_BedRestEffectiveness")]
        public float? BedRestEffectiveness { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BuildingsFromMaterial_WorkTableWorkSpeedFactor")]
        public float? WorkTableWorkSpeedFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BuildingsFromMaterial_ResearchSpeedFactor")]
        public float? ResearchSpeedFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BuildingsFromMaterial_MaxHitPoints")]
        public float? MaxHitPoints { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BuildingsFromMaterial_MarketValue")]
        public float? MarketValue { get; set; }
    }
}