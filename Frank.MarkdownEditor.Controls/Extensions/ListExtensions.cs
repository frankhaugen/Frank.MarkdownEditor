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

}