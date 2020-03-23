using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class WeaponRanged : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("ItemsOnMap")]
        public float? ItemsOnMap { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("WeaponRanged_CanCraft")] //есть рецепт
        public bool CanCraft { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponRanged_DPS")]
        public float? Dps { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponRanged_Rpm")]
        public float? Rpm { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponRanged_Damage")]
        public float? Damage { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponRanged_BurstShotCount")]
        public float? BurstShotCount { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponRanged_Range")]
        public float? MaxRange { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("WeaponRanged_Cooldown")]
        public float? Cooldown { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("WeaponRanged_WarmupTime")]
        public float? WarmupTime { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponRanged_MarketValue")]
        public float? MarketValue { get; set; }

        [LocalizedDisplayName("WeaponRanged_Accuracy")]
        public string Accuracy { get; set; }

        [LocalizedDisplayName("WeaponRanged_TechLevel")]
        public string TechLevel { get; set; }

        [LocalizedDisplayName("WeaponRanged_DamageType")]
        public string DamageType { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("WeaponRanged_CE_OneHanded")]
        public bool CE_OneHanded { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("WeaponRanged_CE_SightsEfficiency")]
        public float? CE_SightsEfficiency { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("WeaponRanged_CE_ShotSpread")]
        public float? CE_ShotSpread { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("WeaponRanged_CE_SwayFactor")]
        public float? CE_SwayFactor { get; set; }

        [LocalizedDisplayName("WeaponRanged_WeaponTags")]
        public string WeaponTags { get; set; }
    }
}