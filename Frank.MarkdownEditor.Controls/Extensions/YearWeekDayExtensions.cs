using System.IO;
using System.Windows.Forms;

namespace Frank.MarkdownEditor.Controls.Contexts;

public static class YearWeekDayExtensions
{
    public static string CreateFileName(this YearWeekDay yearWeekDay, string extension) => $"{yearWeekDay} {yearWeekDay.Day}.{extension.TrimStart('.')}";
    public static FileInfo CreateFileInfo(this YearWeekDay yearWeekDay, DirectoryInfo directory, string extension) => new(Path.Combine(directory.FullName,yearWeekDay.CreateFileName(extension)));
    
    public static Label ToLabel(this YearWeekDay yearWeekDay) => new() {Text = yearWeekDay.ToString()};

}