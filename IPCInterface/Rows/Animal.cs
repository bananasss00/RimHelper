using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class Animal : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_ArmorRating_Sharp")]
        public float? ArmorRating_Sharp { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_ArmorRating_Blunt")]
        public float? ArmorRating_Blunt { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_ArmorRating_Heat")]
        public float? ArmorRating_Heat { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_Mass")]
        public float? Mass { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_MarketValue")]
        public float? MarketValue { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_MeleeDPS_")]
        public float? MeleeDPS_ { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_MeleeArmorPenetration")]
        public float? MeleeArmorPenetration { get; set; }

        //[ColorizeOrder(ColorizeOrderOption.Ascending)]
        //[DisplayName("Шанс уворота в рукопашной")]
        //public float? MeleeDodgeChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_MoveSpeed")]
        public float? MoveSpeed { get; set; }

        //[ColorizeOrder(ColorizeOrderOption.Ascending)]
        //[DisplayName("Чувствительность к токсинам")]
        //public float? ToxicSensitivity { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_EatingSpeed")]
        public float? EatingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_ComfyTemperatureMin")]
        public float? ComfyTemperatureMin { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_ComfyTemperatureMax")]
        public float? ComfyTemperatureMax { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_ImmunityGainSpeed")]
        public float? ImmunityGainSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_CarryingCapacity")]
        public float? CarryingCapacity { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_MeatAmount")]
        public float? MeatAmount { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_LeatherAmount")]
        public float? LeatherAmount { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_MinimumHandlingSkill")]
        public float? MinimumHandlingSkill { get; set; }

        //[ColorizeOrder(ColorizeOrderOption.Ascending)]
        //[DisplayName("Порог переносимости боли")]
        //public float? PainShockThreshold { get; set; }

        //[ColorizeOrder(ColorizeOrderOption.Ascending)]
        //[DisplayName("Изоляция: Холод")]
        //public float? StuffPower_Insulation_Cold { get; set; }

        //[ColorizeOrder(ColorizeOrderOption.Ascending)]
        //[DisplayName("Изоляция: Тепло")]
        //public float? StuffPower_Insulation_Heat { get; set; }

        [LocalizedDisplayName("Animal_MilkDef")]
        public string MilkDef { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Animal_MilkIntervalDays")]
        public float? MilkIntervalDays { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_MilkAmount")]
        public float? MilkAmount { get; set; }

        [LocalizedDisplayName("Animal_WoolDef")]
        public string WoolDef { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Animal_ShearIntervalDays")]
        public float? ShearIntervalDays { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_WoolAmount")]
        public float? WoolAmount { get; set; }

        //CUSTOM
        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Animal_ManhunterOnTameFailChance")]
        public float? ManhunterOnTameFailChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_Wildness")]
        public float? Wildness { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Animal_Petness")]
        public float? Petness { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Animal_Predator")]
        public bool Predator { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Animal_PackAnimal")]
        public bool PackAnimal { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Animal_HerdAnimal")]
        public bool HerdAnimal { get; set; }

        [LocalizedDisplayName("Animal_Trainability")]
        public string Trainability { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Animal_Train1")]
        public bool Train1 { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Animal_Train2")]
        public bool Train2 { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Animal_Train3")]
        public bool Train3 { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Animal_Train4")]
        public bool Train4 { get; set; }
    }
}