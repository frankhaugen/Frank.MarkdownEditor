using Frank.MarkdownEditor.Controls.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Frank.MarkdownEditor.Controls.Contexts;

public class YearWeeksContext
{
    public YearWeeksContext(int year, DirectoryInfo baseDirectory)
    {
        Year = year;
        WeeksInYear = ISOWeek.GetWeeksInYear(year);
        BaseDirectory = baseDirectory;
        YearDirectory = new DirectoryInfo(Path.Combine(baseDirectory.FullName, year.ToString("0000")));
    }

    public int Year { get; }
    public int WeeksInYear { get; }
    public DirectoryInfo BaseDirectory { get; }
    public DirectoryInfo YearDirectory { get; } 

    public Dictionary<YearWeekDay, FileMetadata> CreateFilesForYearWithDictionary(string extension)
    {
        var dictionary = new Dictionary<YearWeekDay, FileMetadata>();
        
        foreach (var week in Enumerable.Range(1, WeeksInYear))
        foreach (var day in Enum.GetValues<DayOfWeek>())
        {
            var yearWeekDay = new YearWeekDay() { Year = Year, Week = week, Day = day };
            var file = CreateFileForDay(yearWeekDay, extension);
            dictionary.Add(yearWeekDay, file.GetMetadata());
        }

        return dictionary;
    }
    
    public DirectoryInfo CreateDirectoryForWeek(YearWeek yearWeek) => new DirectoryInfo(Path.Combine(YearDirectory.FullName, yearWeek.Week.ToString("00")));

    public IEnumerable<FileInfo> CreateFilesForWeek(int week, string extension) => Enum.GetValues<DayOfWeek>().Select(x => CreateFileForDay(new YearWeekDay() {Year = Year, Week = week, Day = x}, extension));
    public IEnumerable<FileInfo> CreateFilesForWeek(YearWeek yearWeek, string extension) => Enum.GetValues<DayOfWeek>().Select(x => CreateFileForDay(new YearWeekDay() { Year = yearWeek.Year, Week = yearWeek.Week, Day = x}, extension));
    public IEnumerable<FileInfo> CreateFilesForYear(string extension) => Enumerable.Range(1, WeeksInYear).SelectMany(x => CreateFilesForWeek(x, extension));
    public IEnumerable<FileInfo> CreateFilesForYear(int year, string extension) => Enumerable.Range(1, ISOWeek.GetWeeksInYear(year)).SelectMany(x => CreateFilesForWeek(x, extension));
    public FileInfo CreateFileForDay(YearWeekDay yearWeekDay, string extension) => yearWeekDay.CreateFileInfo(CreateDirectoryForWeek(yearWeekDay), extension);
}