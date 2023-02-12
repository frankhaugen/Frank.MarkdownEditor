using System.Globalization;
using System.IO;

namespace Frank.MarkdownEditor.Controls.Contexts;

public static partial class FileInfoExtensions
{
    public static FileMetadata GetMetadata(this FileInfo file)
    {
        return new(file);
    }
    
    public static YearWeek GetWeek(this FileInfo file)
    {
        return new()
        {
            Week = ISOWeek.GetWeekOfYear(file.CreationTime),
            Year = file.CreationTime.Year
        };
    }
    
    public static YearWeekDay GetWeekDay(this FileInfo file)
    {
        return new()
        {
            Week = ISOWeek.GetWeekOfYear(file.CreationTime),
            Year = file.CreationTime.Year,
            Day = file.CreationTime.DayOfWeek
        };
    }
    
    public static string GetDisplayName(this FileInfo file)
    {
        return file.Name;
    }
    
    public static bool IsVisible(this FileInfo file)
    {
        return file.Name.StartsWith(".") || !file.Name.EndsWith(".md");
    }
    
}