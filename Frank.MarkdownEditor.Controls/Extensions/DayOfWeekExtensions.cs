using System;
using System.Globalization;
using System.Windows.Forms;

namespace Frank.MarkdownEditor.Controls.Contexts;

public static class DayOfWeekExtensions
{
    
    public static Label ToLabel(this DayOfWeek dayOfWeek) => new() {Text = dayOfWeek.ToShortString()};
    
    public static string ToShortString(this DayOfWeek dayOfWeek) => CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(dayOfWeek);
    public static string ToLongString(this DayOfWeek dayOfWeek) => CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dayOfWeek);
}