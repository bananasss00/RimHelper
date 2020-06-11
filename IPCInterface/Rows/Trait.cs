using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class Trait : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [LocalizedDisplayName("Trait_defName")]
        public string defName { get; set; }

        [LocalizedDisplayName("Trait_conflictingTraits")]
        public string conflictingTraits { get; set; }

        // Skills
        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Melee")] // Ближний бой
        public float? Melee { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Shooting")] // Дальний бой
        public float? Shooting { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Cooking")] // Кулинария
        public float? Cooking { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Medicine")] // Медицина
        public float? Medicine { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Intellectual")] // Умственный труд
        public float? Intellectual { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Artistic")] // Искусство
        public float? Artistic { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Construction")] // Строительство
        public float? Construction { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Social")] // Общение
        public float? Social { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Animals")] // Животноводство
        public float? Animals { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Plants")] // Растениеводство
        public float? Plants { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Crafting")] // Ремесло
        public float? Crafting { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Mining")] // Горное дело
        public float? Mining { get; set; }

        // Stats
        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_Vulnerability")] // Уязвимость
        public float? Vulnerability { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_GlobalLearningFactor")] // Эффективность обучения
        public float? GlobalLearningFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_MentalBreakThreshold")] // Порог нервного срыва
        public float? MentalBreakThreshold { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_MeleeHitChance")] // Шанс удара врукопашную
        public float? MeleeHitChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_MeleeCritChance")] // Шанс критического удара
        public float? MeleeCritChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_MeleeParryChance")] // Шанс парировать в ближнем бою
        public float? MeleeParryChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_SexFrequency")] // Сексуальное влечение
        public float? SexFrequency { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_PainShockThreshold")] // Порог переносимости боли
        public float? PainShockThreshold { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_PawnTrapSpringChance")] // Шанс активации ловушек
        public float? PawnTrapSpringChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_MeleeDodgeChance")] // Шанс уворота в рукопашной
        public float? MeleeDodgeChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_IncomingDamageFactor")] // Множитель получаемого урона
        public float? IncomingDamageFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_SexAbility")] // Сексуальные навыки
        public float? SexAbility { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_HungerRateMultiplier")] // Множитель голода
        public float? HungerRateMultiplier { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_RestRateMultiplier")] // Эффективность сна
        public float? RestRateMultiplier { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_MoveSpeed")] // Скорость передвижения
        public float? MoveSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_WorkSpeedGlobal")] // Общая скорость работы
        public float? WorkSpeedGlobal { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ImmunityGainSpeed")] // Скорость выработки иммунитета
        public float? ImmunityGainSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_Suppressability")] // Вероятность подавления
        public float? Suppressability { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_AimingDelayFactor")] // Время прицеливания
        public float? AimingDelayFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ShootingAccuracyPawn")] // Точность стрельбы
        public float? ShootingAccuracyPawn { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ResearchSpeed")] // Скорость исследования
        public float? ResearchSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ConstructionSpeed")] // Скорость строительства
        public float? ConstructionSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ConstructSuccessChance")] // Шанс успешного строительства
        public float? ConstructSuccessChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_GeneralLaborSpeed")] // Скорость общей работы
        public float? GeneralLaborSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_SculptingSpeed")] // Скорость ваяния
        public float? SculptingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_PsychicSensitivity")] // Психочувствительность
        public float? PsychicSensitivity { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_PawnBeauty")] // Красота
        public float? PawnBeauty { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_NegotiationAbility")] // Способность к переговорам
        public float? NegotiationAbility { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_SocialImpact")] // Социальное влияние
        public float? SocialImpact { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_AnimalGatherSpeed")] // Скорость работы с животными
        public float? AnimalGatherSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_AnimalGatherYield")] // Эффект-ть работы с животными
        public float? AnimalGatherYield { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_TameAnimalChance")] // Шанс приручения животных
        public float? TameAnimalChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_TrainAnimalChance")] // Шанс обучения животных
        public float? TrainAnimalChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_PlantHarvestYield")] // Урожайность сбора
        public float? PlantHarvestYield { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_PlantWorkSpeed")] // Скорость работы с растениями
        public float? PlantWorkSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ButcheryFleshEfficiency")] // Эффективность разделки
        public float? ButcheryFleshEfficiency { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ButcheryFleshSpeed")] // Скорость разделки
        public float? ButcheryFleshSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ButcheryMechanoidEfficiency")] // Эффект-ть разбора механоидов
        public float? ButcheryMechanoidEfficiency { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ButcheryMechanoidSpeed")] // Скорость разбора механоидов
        public float? ButcheryMechanoidSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_DrugCookingSpeed")] // Скорость готовки наркотиков
        public float? DrugCookingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_CookSpeed")] // Скорость приготовления пищи
        public float? CookSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_DrugSynthesisSpeed")] // Скорость синтеза препаратов
        public float? DrugSynthesisSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_MiningSpeed")] // Скорость горных работ
        public float? MiningSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_MiningYield")] // Эффективность добычи
        public float? MiningYield { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ComfyTemperatureMax")] // Макс. комфортная температура
        public float? ComfyTemperatureMax { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_ComfyTemperatureMin")] // Мин. комфортная температура
        public float? ComfyTemperatureMin { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_FoodPoisonChance")] // Шанс отравления пищи
        public float? FoodPoisonChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_FixBrokenDownBuildingSuccessChance")] // Шанс успешного ремонта
        public float? FixBrokenDownBuildingSuccessChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_MedicalSurgerySuccessChance")] // Шанс успеха операции
        public float? MedicalSurgerySuccessChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_MedicalTendQuality")] // Качество лечения
        public float? MedicalTendQuality { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_MedicalOperationSpeed")] // Скорость проведения медпроцедур
        public float? MedicalOperationSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_MedicalTendSpeed")] // Скорость лечения
        public float? MedicalTendSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_GermContainment")] // Стерильность
        public float? GermContainment { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_GermResistance")] // Устойчивость к болезням
        public float? GermResistance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_EatingSpeed")] // Скорость поглощения пищи
        public float? EatingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_SmithingSpeed")] // Скорость ковки
        public float? SmithingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_TailoringSpeed")] // Скорость шитья
        public float? TailoringSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_SmoothingSpeed")] // Скорость выравнивания
        public float? SmoothingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_TradePriceImprovement")] // Улучшение торговой цены
        public float? TradePriceImprovement { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_ToxicSensitivity")] // Чувствительность к токсинам
        public float? ToxicSensitivity { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_CarryWeight")] // Переносимый вес
        public float? CarryWeight { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_HuntingStealth")] // Скрытность на охоте
        public float? HuntingStealth { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ElectronicCraftingSpeed")] // Скорость создания электроники
        public float? ElectronicCraftingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_UnskilledLaborSpeed")] // Скорость простой работы
        public float? UnskilledLaborSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_CleaningSpeed")] // Скорость уборки
        public float? CleaningSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_CollectingSpeed")] // Скорость сбора
        public float? CollectingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ForagedNutritionPerDay")] // Количество добываемой еды
        public float? ForagedNutritionPerDay { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_SmeltingSpeed")] // Скорость переплавки
        public float? SmeltingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_TreeFellingSpeed")] // Скорость рубки деревьев
        public float? TreeFellingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_CarryingCapacity")] // Грузоподъёмность
        public float? CarryingCapacity { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_CarryBulk")] // Вмещаемый объём
        public float? CarryBulk { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_FishingSpeed")] // Скорость рыбалки
        public float? FishingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ReloadSpeed")] // Скорость перезарядки
        public float? ReloadSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_PlantHarvestingSpeed")] // Скорость сбора растений
        public float? PlantHarvestingSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_AimingAccuracy")] // Точность прицеливания
        public float? AimingAccuracy { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_ConstructionResearchSpeed")] // Скорость строительных исследований
        public float? ConstructionResearchSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Trait_CraftingResearchSpeed")] // Скорость ремесленных исследований
        public float? CraftingResearchSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_Flammability")] // Воспламеняемость
        public float? Flammability { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Descending)]
        [LocalizedDisplayName("Trait_Radiation")] // Восприимчивость к радиации
        public float? Radiation { get; set; }

        [LocalizedDisplayName("Trait_disabledWorkTypes")]
        public string disabledWorkTypes { get; set; }

        [LocalizedDisplayName("Trait_disabledWorkTags")]
        public string disabledWorkTags { get; set; }

        [LocalizedDisplayName("Trait_requiredWorkTypes")]
        public string requiredWorkTypes { get; set; }

        [LocalizedDisplayName("Trait_requiredWorkTags")]
        public string requiredWorkTags { get; set; }

    }
}