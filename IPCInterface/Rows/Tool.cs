using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    /*
     SurvivalTools:
         TreeFellingSpeed;Скорость рубки деревьев
         DiggingSpeed;Скорость копания
         MiningYieldDigging;Добыча полезных ископаемых
         ConstructionSpeed;Скорость строительства
         SmithingSpeed;Скорость ковки
         PlantHarvestingSpeed;Скорость сбора растений
         PlantSowingSpeed;Скорость посадки растений
        Default:
         CleaningSpeed;Скорость уборки
         Suppressability;Вероятность подавления
         MedicalOperationSpeed;Скорость проведения медпроцедур
         MedicalSurgerySuccessChance;Шанс успеха операции
         MeleeCritChance;Шанс критического удара
         MeleeDodgeChance;Шанс уворота в рукопашной
         MeleeParryChance;Шанс парировать в ближнем бою
         PlantWorkSpeed;Скорость работы с растениями
     */
    [Serializable]
    public class ST_Tool : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        //SurvivalTools:
        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_TreeFellingSpeed")]
        public float? TreeFellingSpeed { get; set; } //Скорость рубки деревьев

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_DiggingSpeed")]
        public float? DiggingSpeed { get; set; } //Скорость копания

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_MiningYieldDigging")]
        public float? MiningYieldDigging { get; set; } //Добыча полезных ископаемых

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_ConstructionSpeed")]
        public float? ConstructionSpeed { get; set; } //Скорость строительства

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_SmithingSpeed")]
        public float? SmithingSpeed { get; set; } //Скорость ковки

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_PlantHarvestingSpeed")]
        public float? PlantHarvestingSpeed { get; set; } //Скорость сбора растений

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_PlantSowingSpeed")]
        public float? PlantSowingSpeed { get; set; } //Скорость посадки растений

        //RightToolForJob:
        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_CleaningSpeed")]
        public float? CleaningSpeed { get; set; } //Скорость уборки

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_Suppressability")]
        public float? Suppressability { get; set; } //Вероятность подавления

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_MedicalOperationSpeed")]
        public float? MedicalOperationSpeed { get; set; } //Скорость проведения медпроцедур

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_MedicalSurgerySuccessChance")]
        public float? MedicalSurgerySuccessChance { get; set; } //Шанс успеха операции

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_MeleeCritChance")]
        public float? MeleeCritChance { get; set; } //Шанс критического удара

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_MeleeDodgeChance")]
        public float? MeleeDodgeChance { get; set; } //Шанс уворота в рукопашной

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_MeleeParryChance")]
        public float? MeleeParryChance { get; set; } //Шанс парировать в ближнем бою

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_PlantWorkSpeed")]
        public float? PlantWorkSpeed { get; set; } //Скорость работы с растениями

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_CookSpeed")]
        public float? CookSpeed { get; set; } //Скорость приготовления пищи

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_ButcheryFleshSpeed")]
        public float? ButcheryFleshSpeed { get; set; } //Скорость разделки

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_GermContainment")] // Стерильность
        public float? GermContainment { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Tool_GermResistance")] // Устойчивость к болезням
        public float? GermResistance { get; set; }
    }
}