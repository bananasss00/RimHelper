using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class Material : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("ItemsOnMap")]
        public float? ItemsOnMap { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_StuffPower_Armor_Sharp")]
        public float? StuffPower_Armor_Sharp { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_StuffPower_Armor_Blunt")]
        public float? StuffPower_Armor_Blunt { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_StuffPower_Armor_Heat")]
        public float? StuffPower_Armor_Heat { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_StuffPower_Insulation_Cold")]
        public float? StuffPower_Insulation_Cold { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_StuffPower_Insulation_Heat")]
        public float? StuffPower_Insulation_Heat { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_SharpDamageMultiplier")]
        public float? SharpDamageMultiplier { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_BluntDamageMultiplier")]
        public float? BluntDamageMultiplier { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Material_MeleeWeapon_CooldownMultiplier")]
        public float? MeleeWeapon_CooldownMultiplier { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_HitPoints")]
        public float? HitPoints { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_ConstructionSpeedFactor")]
        public float? ConstructionSpeedFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_DoorOpenSpeed")]
        public float? DoorOpenSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Material_DeteriorationRate")]
        public float? DeteriorationRate { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_Beauty")]
        public float? Beauty { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Material_Flammability")]
        public float? Flammability { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Material_Mass")]
        public float? Mass { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_MaxHitPoints")]
        public float? MaxHitPoints { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Material_WorkToMake")]
        public float? WorkToMake { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_MarketValue")]
        public float? MarketValue { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_ImmunityGainSpeedFactor")]
        public float? ImmunityGainSpeedFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_BedRestEffectiveness")]
        public float? BedRestEffectiveness { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_WorkTableWorkSpeedFactor")]
        public float? WorkTableWorkSpeedFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_ResearchSpeedFactor")]
        public float? ResearchSpeedFactor { get; set; }

        //[ColorizeOrder(ColorizeOrderOption.Descending)]
        //[DisplayName("Чистота")]
        //public float? Cleanliness { get; set; }

        //[ColorizeOrder(ColorizeOrderOption.Descending)]
        //[DisplayName("Шанс отравления пищи")]
        //public float? FoodPoisonChanceFixedHuman { get; set; }

        //[ColorizeOrder(ColorizeOrderOption.Descending)]
        //[DisplayName("Объём работ1")]
        //public float? WorkToBuild { get; set; }

        //[ColorizeOrder(ColorizeOrderOption.Descending)]
        //[DisplayName("Бронепробитие врукопашную")]
        //public float? MeleeWeapon_AverageArmorPenetration { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Material_CE_MeleePenetrationFactor")]
        public float? CE_MeleePenetrationFactor { get; set; }

        [LocalizedDisplayName("Material_Category")]
        public string Category { get; set; }
    }
}