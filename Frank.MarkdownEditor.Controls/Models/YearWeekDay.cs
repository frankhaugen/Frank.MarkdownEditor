using System;
using System.Globalization;
using System.IO;

namespace Frank.MarkdownEditor.Controls.Contexts;

public class YearWeekDay : YearWeek
{
    public DayOfWeek Day { get; init; }
    
    public override string ToString() => $"{Year:0000}-{Week:00}-{Day:D}";
    
    public static YearWeekDay Parse(string value)
    {
        var parts = value.Split('-');
        return new()
        {
            Year = int.Parse(parts[0]),
            Week = int.Parse(parts[1]),
            Day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), parts[2])
        };
    }
    
    public static bool TryParse(string value, out YearWeekDay result)
    {
        try
        {
            result = Parse(value);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
    
    public static YearWeekDay FromDateTime(DateTime dateTime)
    {
        return new()
        {
            Year = dateTime.Year,
            Week = ISOWeek.GetWeekOfYear(dateTime),
            Day = dateTime.DayOfWeek
        };
    }
    
    public static YearWeekDay FromFileInfo(FileInfo file)
    {
        return FromDateTime(file.CreationTime);
    }
    
    
    public static bool operator ==(YearWeekDay left, YearWeekDay right) => left.Equals(right);
    public static bool operator !=(YearWeekDay left, YearWeekDay right) => !left.Equals(right);
    public static bool operator >(YearWeekDay left, YearWeekDay right) => left.CompareTo(right) > 0;
    public static bool operator <(YearWeekDay left, YearWeekDay right) => left.CompareTo(right) < 0;
    public static bool operator >=(YearWeekDay left, YearWeekDay right) => left.CompareTo(right) >= 0;
    public static bool operator <=(YearWeekDay left, YearWeekDay right) => left.CompareTo(right) <= 0;
    
    public override bool Equals(object? obj) => obj is YearWeekDay other && Equals(other);
    public bool Equals(YearWeekDay other) => Year == other.Year && Week == other.Week && Day == other.Day;
    public override int GetHashCode() => HashCode.Combine(Year, Week, Day);
    public int CompareTo(YearWeekDay other)
    {
        var yearComparison = Year.CompareTo(other.Year);
        if (yearComparison != 0) return yearComparison;
        var weekComparison = Week.CompareTo(other.Week);
        if (weekComparison != 0) return weekComparison;
        return Day.CompareTo(other.Day);
    }
    
    public static YearWeekDay operator +(YearWeekDay left, int right) => FromDateTime(left.ToDateTime().AddDays(right));
    public static YearWeekDay operator -(YearWeekDay left, int right) => FromDateTime(left.ToDateTime().AddDays(-right));
    public static int operator -(YearWeekDay left, YearWeekDay right) => (left.ToDateTime() - right.ToDateTime()).Days;
    
    // Convert to DateTime with operators
    public static implicit operator DateTime(YearWeekDay yearWeekDay) => yearWeekDay.ToDateTime();
    public static implicit operator YearWeekDay(DateTime dateTime) => FromDateTime(dateTime);
    
    // Convert to DateOnly with operators
    public static implicit operator DateOnly(YearWeekDay yearWeekDay) => yearWeekDay.ToDateOnly();
    public static implicit operator YearWeekDay(DateOnly dateOnly) => FromDateOnly(dateOnly);
    
    public DateOnly ToDateOnly() => new DateOnly(Year, 1, 1).AddDays(7 * (Week - 1)).AddDays((int)Day);


    public DateTime ToDateTime()
    {
        var firstDayOfYear = new DateTime(Year, 1, 1);
        var firstDayOfWeek = firstDayOfYear.AddDays(-(int)firstDayOfYear.DayOfWeek);
        var firstDayOfFirstWeek = firstDayOfWeek.AddDays(1);
        var firstDayOfTargetWeek = firstDayOfFirstWeek.AddDays(7 * (Week - 1));
        var targetDay = firstDayOfTargetWeek.AddDays((int)Day);
        return targetDay;
    }
    
       public static YearWeekDay FromDateOnly(DateOnly dateOnly)
        {
            return new()
            {
                Year = dateOnly.Year,
                Week = dateOnly.GetWeekInYear(),
                Day = dateOnly.DayOfWeek
            };
        }
    
}