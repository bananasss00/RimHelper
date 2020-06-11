using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class Apparel : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("ItemsOnMap")]
        public float? ItemsOnMap { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Positive)]
        [LocalizedDisplayName("Apparel_CanCraft")] //есть рецепт
        public bool CanCraft { get; set; }

        [LocalizedDisplayName("Apparel_Material")]
        public string Material { get; set; }

        [LocalizedDisplayName("Apparel_DefMaterial")]
        public string DefMaterial { get; set; }

        [LocalizedDisplayName("Apparel_Body")]
        public string Body { get; set; }

        [LocalizedDisplayName("Apparel_Layer")]
        public string Layer { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_ArmorBlunt")]
        public float? ArmorBlunt { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_ArmorSharp")]
        public float? ArmorSharp { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Apparel_ArmorHeat")]
        public float? ArmorHeat { get; set; }
        
        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Apparel_InsulationCold")]
        public float? InsulationCold { get; set; }
        
        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Apparel_InsulationHeat")]
        public float? InsulationHeat { get; set; }
        
        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MarketValue")]
        public float? MarketValue_ { get; set; }

        // STATDEFS
        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MoveSpeed")] //скорость передвижения
        public float? MoveSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_Suppressability")] //вероятность подавления
        public float? Suppressability { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_WorkSpeedGlobal")] //общая скорость работы
        public float? WorkSpeedGlobal { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_SocialImpact")] //социальное влияние
        public float? SocialImpact { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_PsychicSensitivity")] //психочувствительность
        public float? PsychicSensitivity { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_NegotiationAbility")] //способность к переговорам
        public float? NegotiationAbility { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_TradePriceImprovement")] //улучшение торговой цены
        public float? TradePriceImprovement { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MentalBreakThreshold")] //порог нервного срыва
        public float? MentalBreakThreshold { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_ShootingAccuracyPawn")] //точность стрельбы
        public float? ShootingAccuracyPawn { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_AimingDelayFactor")] //время прицеливания
        public float? AimingDelayFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MeleeHitChance")] //шанс удара врукопашную
        public float? MeleeHitChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_GlobalLearningFactor")] //общий фактор обучения
        public float? GlobalLearningFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MedicalOperationSpeed")] //скорость проведения операций
        public float? MedicalOperationSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MedicalTendSpeed")] //скорость лечения
        public float? MedicalTendSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MedicalTendQuality")] //качество лечения
        public float? MedicalTendQuality { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MedicalSurgerySuccessChance")] //шанс успеха операции
        public float? MedicalSurgerySuccessChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_CarryBulk")] //вмещаемый объём
        public float? CarryBulk { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MeleeWeapon_CooldownMultiplier")] //время между ударами
        public float? MeleeWeapon_CooldownMultiplier { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_CookSpeed")] //скорость приготовления пищи
        public float? CookSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_FoodPoisonChance")] //шанс отравления пищи
        public float? FoodPoisonChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_DrugCookingSpeed")] //скорость готовки наркотиков
        public float? DrugCookingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_ButcheryFleshSpeed")] //скорость разделки
        public float? ButcheryFleshSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_TrainAnimalChance")] //шанс обучения животных
        public float? TrainAnimalChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_TameAnimalChance")] //шанс приручения животных
        public float? TameAnimalChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_CarryWeight")] //переносимый вес
        public float? CarryWeight { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_ImmunityGainSpeed")] //скорость выработки иммунитета
        public float? ImmunityGainSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_ConstructionSpeed")] //скорость строительства
        public float? ConstructionSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_SmeltingSpeed")] //скорость переплавки
        public float? SmeltingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_PlantWorkSpeed")] //скорость работы с растениями
        public float? PlantWorkSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MiningSpeed")] //скорость горных работ
        public float? MiningSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_StonecuttingSpeed")] //скорость обработки камней
        public float? StonecuttingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_ElectronicCraftingSpeed")] //Скорость создания электроники
        public float? ElectronicCraftingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_ReloadSpeed")] //скорость перезарядки
        public float? ReloadSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_FixBrokenDownBuildingSuccessChance")] //шанс успешного ремонта
        public float? FixBrokenDownBuildingSuccessChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_SmithingSpeed")] //скорость ковки
        public float? SmithingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_SmoothingSpeed")] //скорость выравнивания
        public float? SmoothingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_SculptingSpeed")] //скорость ваяния
        public float? SculptingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_TailoringSpeed")] //скорость шитья
        public float? TailoringSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_ButcheryFleshEfficiency")] //эффективность разделки
        public float? ButcheryFleshEfficiency { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_AimingAccuracy")] //точность прицеливания
        public float? AimingAccuracy { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MeleeCritChance")] //шанс критического удара
        public float? MeleeCritChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MeleeParryChance")] //шанс парировать в ближнем бою
        public float? MeleeParryChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_MeleeDodgeChance")] //Шанс уворота в рукопашной
        public float? MeleeDodgeChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_ArmorRating_Toxin")] //защита - токсин
        public float? ArmorRating_Toxin { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_SurvivalToolCarryCapacity")] //Вместимость инструментов
        public float? SurvivalToolCarryCapacity { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Apparel_Radiation")] //Восприимчивость к радиации
        public float? Radiation { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_GermResistance")] //Устойчивость к болезням
        public float? GermResistance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_GermContainment")] //Стерильность
        public float? GermContainment { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Apparel_SmokeSensitivity")] //Чувствительность к дыму
        public float? SmokeSensitivity { get; set; }
    }
}