using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;

namespace IPCInterface
{
    [Serializable]
    public class RowBase
    {
        private static int _lastId = 0;

        public RowBase() => Id = ++_lastId;
        ~RowBase() => _lastId = 0;

        public int Id { get; } // unique rowId

        [Browsable(false)] public string Description { get; set; }

        [Browsable(false)] public string TexturePath { get; set; }
    }

    public enum ColorizeOrderOption
    {
        None, // none
        Ascending, //up
        Descending, // down
        Positive,
        Bool
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ColorizeOrderAttribute : Attribute
    {
        public ColorizeOrderOption Order { get; set; }

        public ColorizeOrderAttribute()
        {
            Order = ColorizeOrderOption.None;
        }

        public ColorizeOrderAttribute(ColorizeOrderOption order)
        {
            Order = order;
        }
    }
}