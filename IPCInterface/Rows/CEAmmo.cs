using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class CEAmmo : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        //[ColorizeOrder(ColorizeOrderOption.Positive)]
        //[LocalizedDisplayName("ItemsOnMap")]
        //public bool ItemsOnMap { get; set; }

        [LocalizedDisplayName("CEAmmo_DamageType")]
        public string DamageType { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("CEAmmo_Damage")]
        public float? Damage { get; set; }
		
		[ColorizeOrder(ColorizeOrderOption.Ascending)] // OUTDATED < CE 1.9
        [LocalizedDisplayName("CEAmmo_ArmorPenetration")]
        public float? ArmorPenetration { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)] // NEW >= CE 1.9
        [LocalizedDisplayName("CEAmmo_ArmorPenetrationSharp")]
        public float? ArmorPenetrationSharp { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)] // NEW >= CE 1.9
        [LocalizedDisplayName("CEAmmo_ArmorPenetrationBlunt")]
        public float? ArmorPenetrationBlunt { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("CEAmmo_Speed")]
        public float? Speed { get; set; }

        [LocalizedDisplayName("CEAmmo_DamageType2")]
        public string DamageType2 { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("CEAmmo_Damage2")]
        public float? Damage2 { get; set; }
    }
}