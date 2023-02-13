using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Frank.MarkdownEditor.Controls.Contexts;

public static class YearWeekDayExtensions
{
    public static string CreateFileName(this YearWeekDay yearWeekDay, string extension) => $"{yearWeekDay} {yearWeekDay.Day}.{extension.TrimStart('.')}";
    public static FileInfo CreateFileInfo(this YearWeekDay yearWeekDay, DirectoryInfo directory, string extension) => new(Path.Combine(directory.FullName,yearWeekDay.CreateFileName(extension)));
    
    public static Label ToLabel(this YearWeekDay yearWeekDay) => new() {Text = yearWeekDay.ToString()};

}

public static class DayOfWeekExtensions
{
    
    public static Label ToLabel(this DayOfWeek dayOfWeek) => new() {Text = dayOfWeek.ToShortString()};
    
    public static string ToShortString(this DayOfWeek dayOfWeek) => CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(dayOfWeek);
    public static string ToLongString(this DayOfWeek dayOfWeek) => CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dayOfWeek);
}