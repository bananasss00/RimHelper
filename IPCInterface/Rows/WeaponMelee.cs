using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class WeaponMelee : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("ItemsOnMap")]
        public float? ItemsOnMap { get; set; }

        [LocalizedDisplayName("TechLevel")]
        public string TechLevel { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("WeaponMelee_CanCraft")] //есть рецепт
        public bool CanCraft { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponMelee_DPS")]
        public float? Dps { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponMelee_Damage")]
        public float? Damage { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("WeaponMelee_Cooldown")]
        public float? Cooldown { get; set; }
        
        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponMelee_MarketValue")]
        public float? MarketValue { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponMelee_MeleeCritChance")]
        public float? MeleeCritChance { get; set; } //Шанс критического удара

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponMelee_MeleeDodgeChance")]
        public float? MeleeDodgeChance { get; set; } //Шанс уворота в рукопашной

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponMelee_MeleeParryChance")]
        public float? MeleeParryChance { get; set; } //Шанс парировать в ближнем бою

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("WeaponMelee_CE_OneHanded")]
        public bool CE_OneHanded { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponMelee_CE_MeleeCounterParryBonus")]
        public float? CE_MeleeCounterParryBonus { get; set; }

        [LocalizedDisplayName("WeaponMelee_WeaponTags")]
        public string WeaponTags { get; set; }
    }
}