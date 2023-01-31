using System;
using System.Collections.Generic;
using System.Linq;

namespace Frank.MarkdownEditor.Controls.Extensions;

public static class ListExtensions
{
    public static List<T> ClearAnd<T>(this List<T> source)
    {
        source.Clear();
        return source;
    }

    public static void Then<T>(this T source, Action action) => action.Invoke();

    public static IEnumerable<T> Append<T>(this IEnumerable<T> source, IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            source.Append(value);
        }

        return source;
    }
}