using System;
using System.ComponentModel;

namespace IPCInterface.Rows
{
    [Serializable]
    public class Backstory : RowBase
    {
        [LocalizedDisplayName("Label")]
        public string Label { get; set; }

        [LocalizedDisplayName("Backstory_BackstorySlot")]
        public string BackstorySlot { get; set; }

        // Skills
        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Intellectual")] // Умственный труд
        public float? Intellectual { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Shooting")] // Дальний бой
        public float? Shooting { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Melee")] // Ближний бой
        public float? Melee { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Social")] // Общение
        public float? Social { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Artistic")] // Искусство
        public float? Artistic { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Crafting")] // Ремесло
        public float? Crafting { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Medicine")] // Медицина
        public float? Medicine { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Construction")] // Строительство
        public float? Construction { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Mining")] // Горное дело
        public float? Mining { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Animals")] // Животноводство
        public float? Animals { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Plants")] // Растениеводство
        public float? Plants { get; set; }

        [ColorizeOrder(ColorizeOrderOption.Ascending)]
        [LocalizedDisplayName("Backstory_Cooking")] // Кулинария
        public float? Cooking { get; set; }

        [LocalizedDisplayName("Backstory_spawnCategories")]
        public string spawnCategories { get; set; }

        [LocalizedDisplayName("Backstory_DisabledWorkGivers")]
        public string DisabledWorkGivers { get; set; }

        [LocalizedDisplayName("Backstory_DisabledWorkTypes")]
        public string DisabledWorkTypes { get; set; }

        [LocalizedDisplayName("Backstory_disallowedTraits")]
        public string disallowedTraits { get; set; }

        [LocalizedDisplayName("Backstory_forcedTraits")]
        public string forcedTraits { get; set; }

        [LocalizedDisplayName("Backstory_workDisables")]
        public string workDisables { get; set; }

    }
}