using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class Plant : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Plant_NutritionPer10Days")]
        public float? NutritionPer10Days { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Plant_MarketValuePer10Days")]
        public float? MarketValuePer10Days { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Plant_GrowDays")]
        public float? GrowDays { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Plant_GrowMinGlow")]
        public float? GrowMinGlow { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Plant_FertilityMin")]
        public float? FertilityMin { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Plant_FertilitySensitivity")]
        public float? FertilitySensitivity { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Plant_HarvestYield")]
        public float? HarvestYield { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Plant_LifespanDays")]
        public float? LifespanDays { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Plant_Sowable")]
        public bool Sowable { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Plant_SowMinSkill")]
        public float? SowMinSkill { get; set; }

        [LocalizedDisplayName("Plant_HarvestedThingDef")]
        public string HarvestedThingDef { get; set; }


        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Plant_ProductNutrition")]
        public float? ProductNutrition { get; set; }
    
        [LocalizedDisplayName("Plant_ProductPreferability")]
        public string ProductPreferability { get; set; }
        
        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Plant_ProductDaysToRotStart")]
        public float? ProductDaysToRotStart { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Plant_ProductMarketValue")]
        public float? ProductMarketValue { get; set; }
    }
}