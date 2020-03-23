using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class Facility : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_Max")] // Максимум
        public float? Max { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_WorkTableWorkSpeedFactor")] // Скорость работы
        public float? WorkTableWorkSpeedFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_MedicalTendQuality")] // Качество лечения
        public float? MedicalTendQuality { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_ImmunityGainSpeedFactor")] // Скорость выработки иммунитета
        public float? ImmunityGainSpeedFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_MedicalOperationSpeed")] // Скорость проведения медпроцедур
        public float? MedicalOperationSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_MedicalSurgerySuccessChance")] // Шанс успеха операции
        public float? MedicalSurgerySuccessChance { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_MedicalTendSpeed")] // Скорость лечения
        public float? MedicalTendSpeed { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_ResearchSpeedFactor")] // Скорость исследования
        public float? ResearchSpeedFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_LearningSpeedFactor")] // Фактор скорости обучения
        public float? LearningSpeedFactor { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_Comfort")] // Комфорт
        public float? Comfort { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_BedRestEffectiveness")] // Эффективность отдыха
        public float? BedRestEffectiveness { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Facility_MedicalTendQualityOffset")] // Прирост качества лечения
        public float? MedicalTendQualityOffset { get; set; }

        //[ColorizeOrder(ColorizeOrderOption.Ascending)]
        //[LocalizedDisplayName("Facility_Beauty")] // Привлекательность
        //public float? Beauty { get; set; }
    }
}