using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class BodyPart : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_Efficiency")] // Эффективность
        public float? Efficiency { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_Manipulation")] // Работа
        public float? Manipulation { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_Sight")] // Зрение
        public float? Sight { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_Talking")] // Речь
        public float? Talking { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_Eating")] // Питание
        public float? Eating { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_Hearing")] // Слух
        public float? Hearing { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_Moving")] // Движение
        public float? Moving { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_Breathing")] // Дыхание
        public float? Breathing { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_BloodFiltration")] // Фильтрация крови
        public float? BloodFiltration { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_BloodPumping")] // Кровообращение
        public float? BloodPumping { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_Metabolism")] // Метаболизм
        public float? Metabolism { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("BodyPart_Consciousness")] // Сознание
        public float? Consciousness { get; set; }
    }
}