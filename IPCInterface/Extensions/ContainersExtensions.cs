using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IPCInterface.Extensions
{
    public static class ContainersExtensions
    {
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source) => source ?? Enumerable.Empty<T>();

        public static void Dump<T>(this IList<T> rows, string outputFileName)
        {
            var props = typeof(T).GetProperties();
            if (props != null)
            {
                var file = new StringBuilder();

                foreach (var row in rows)
                {
                    foreach (var prop in props)
                    {
                        var value = prop.GetValue(row, null);
                        if (value != null)
                            file.AppendLine($"{prop.Name} = {value}");
                    }

                    file.AppendLine("===new_row===");
                }

                File.WriteAllText(outputFileName, file.ToString());
            }
        }
    }
}