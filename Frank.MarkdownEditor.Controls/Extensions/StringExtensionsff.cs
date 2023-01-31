using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Frank.MarkdownEditor.Controls.Extensions;

public static class StringExtensionsff
{
    public static string ToSentenceCase(this string str) => Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");

    public static string ToTitleCase(this string str)
    {
        var sentenceCase = Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
        var textInfo = new CultureInfo("en-US", false).TextInfo;
        return textInfo.ToTitleCase(sentenceCase);
    }

    public static string ToCamelCase(this string source)
    {
        var firstCharacter = source.First().ToString().ToLower();

        source = source.Remove(0, 1);
        source = firstCharacter + source;



        return source;
    }

    public static string Remove(this string source, string value) => source.Replace(value, "");

    public static string FallbackIfEmpty(this string source, string value)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            return value;
        }

        return source;
    }

    public static T ParseEnum<T>(this string? enumValue, T defaultValue)
    {
        if (Enum.TryParse(typeof(T), enumValue, true, out var parsedValue) && parsedValue != null)
        {
            return (T)parsedValue;
        }

        var type = typeof(T);

        foreach (var field in type.GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute descriptionAttribute && descriptionAttribute.Description?.Equals(enumValue, StringComparison.InvariantCultureIgnoreCase) == true)
            {
                return (T?)field.GetValue(null);
            }

            if (Attribute.GetCustomAttribute(field, typeof(DisplayNameAttribute)) is DisplayNameAttribute displayNameAttribute && displayNameAttribute.DisplayName?.Equals(enumValue, StringComparison.InvariantCultureIgnoreCase) == true)
            {
                return (T?)field.GetValue(null);
            }

            if (field.Name == enumValue)
                return (T?)field.GetValue(null);
        }

        return defaultValue;
    }
}