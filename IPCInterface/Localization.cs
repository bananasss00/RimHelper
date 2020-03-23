using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;

namespace IPCInterface
{
    public static class Localization
    {
        public static Dictionary<string, string> Columns { get; set; }

        public static bool TryLoadFromFile(string fileName, out Exception exception)
        {
            exception = null;

            try
            {
                var xml = XElement.Load(fileName);
                _columns = xml.Element("Columns");
                _gui = xml.Element("GUI");
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }

            return true;
        }

        public static void LoadColumns()
        {
            Columns = new Dictionary<string, string>();

            foreach (var column in _columns.Elements())
            {
                Columns.Add(column.Name.LocalName, column.Value);
            }
        }

        public static void Finalize()
        {
            _columns = null;
            _gui = null;
        }

        public static string Translate(this string srcString) => _gui.Element(srcString)?.Value ?? srcString;

        private static XElement _columns, _gui;
    }

    public sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private PropertyInfo _nameProperty;

        public LocalizedDisplayNameAttribute(string displayNameKey) : base(displayNameKey)
        {
            _nameProperty = typeof(Localization).GetProperty("Columns", BindingFlags.Static | BindingFlags.Public);
        }

        public override string DisplayName
        {
            get
            {
                if (!Localization.Columns.ContainsKey(base.DisplayName))
                {
                    throw new Exception($"Current Localization not contain key: { base.DisplayName }");
                }
                return Localization.Columns[base.DisplayName];
            }
        }
    }
}