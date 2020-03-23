using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Verse;


namespace RimHelperProxyMod.Extensions
{
    public static class ObjectDumperExtensions
    {
        public static string Dump(this object element, int depth = -1)
        {
            return ObjectDumper.Dump(element, 2, depth);
        }
    }

    /*
     * https://stackoverflow.com/posts/10478008/revisions
     * Added exception handling
     * Added flag for private members, in dump before member name -> (*)
     * Added Dump depth
     */
    public class ObjectDumper
    {
        public static bool ThrowExceptions { get; set; } = true; // Continue work after exceptions in some getters
        public static bool IncludePrivateMembers { get; set; } = false; // Show in dump private members
        public static bool ReplaceNewLineSymbol { get; set; } = false; // Fix Code Folding in SublimeText

        private int _depth;
        private int _level;
        private readonly int _indentSize;
        private readonly StringBuilder _stringBuilder;
        private readonly List<int> _hashListOfFoundElements;

        private ObjectDumper(int indentSize, int depth)
        {
            _depth = depth;
            _indentSize = indentSize;
            _stringBuilder = new StringBuilder();
            _hashListOfFoundElements = new List<int>();
        }

        public static string Dump(object element, int indentSize, int depth)
        {
            var instance = new ObjectDumper(indentSize, depth);
            return instance.DumpElement(element);
        }

        private string DumpElement(object element)
        {
            if (_depth != -1 && _level / 2 > _depth)
            {
                Write("#DEPTH_LIMIT#");
                return string.Empty;
            }

            if (element == null || element is ValueType || element is string)
            {
                Write(FormatValue(element));
            }
            else
            {
                var objectType = element.GetType();
                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    Write("{{{0}}}", objectType.FullName);
                    _hashListOfFoundElements.Add(element.GetHashCode());
                    _level++;
                }

                var enumerableElement = element as IEnumerable;
                if (enumerableElement != null)
                {
                    foreach (object item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            _level++;
                            DumpElement(item);
                            _level--;
                        }
                        else
                        {
                            if (!AlreadyTouched(item))
                                DumpElement(item);
                            else
                                Write("{{{0}}} <-- bidirectional reference found", item.GetType().FullName);
                        }
                    }
                }
                else
                {
                    BindingFlags flags = IncludePrivateMembers
                        ? BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic
                        : BindingFlags.Public | BindingFlags.Instance;

                    MemberInfo[] members = element.GetType().GetMembers(flags);
                    foreach (var memberInfo in members)
                    {
                        var fieldInfo = memberInfo as FieldInfo;
                        var propertyInfo = memberInfo as PropertyInfo;

                        if (fieldInfo == null && propertyInfo == null)
                            continue;

                        try
                        {
                            var type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                            object value = fieldInfo != null ? fieldInfo.GetValue(element) : propertyInfo.GetValue(element, null);

                            if (type.IsValueType || type == typeof(string))
                            {
                                bool isPrivate = fieldInfo != null && fieldInfo.IsPrivate;
                                Write("{0}: {1}", (isPrivate ? "(*)" : "") + memberInfo.Name, FormatValue(value));
                            }
                            else
                            {
                                var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
                                bool isPrivate = fieldInfo != null && fieldInfo.IsPrivate;
                                Write("{0}: {1}", (isPrivate ? "(*)" : "") + memberInfo.Name, isEnumerable ? "..." : "{ }");

                                var alreadyTouched = !isEnumerable && AlreadyTouched(value);
                                _level++;
                                if (!alreadyTouched)
                                    DumpElement(value);
                                else
                                    Write("{{{0}}} <-- bidirectional reference found", value.GetType().FullName);
                                _level--;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ThrowExceptions)
                                throw ex;

                            Write("{0}: #EXCEPTION#", propertyInfo.Name);
                        }
                    }
                }

                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    _level--;
                }
            }

            return _stringBuilder.ToString();
        }

        private bool AlreadyTouched(object value)
        {
            if (value == null)
                return false;

            var hash = value.GetHashCode();
            for (var i = 0; i < _hashListOfFoundElements.Count; i++)
            {
                if (_hashListOfFoundElements[i] == hash)
                    return true;
            }
            return false;
        }

        private void Write(string value, params object[] args)
        {
            var space = new string(' ', _level * _indentSize);

            if (args != null)
                value = string.Format(value, args);

            _stringBuilder.AppendLine(space + value);
        }

        private string FormatValue(object o)
        {
            if (o == null)
                return ("null");

            if (o is DateTime)
                return (((DateTime)o).ToShortDateString());

            if (o is string)
                return string.Format("\"{0}\"", ReplaceNewLineSymbol ? ((string)o).Replace("\r", "\\r").Replace("\n", "\\n") : o);

            if (o is char && (char)o == '\0')
                return string.Empty;

            if (o is ValueType)
                return (o.ToString());

            if (o is IEnumerable)
                return ("...");

            return ("{ }");
        }
    }
}