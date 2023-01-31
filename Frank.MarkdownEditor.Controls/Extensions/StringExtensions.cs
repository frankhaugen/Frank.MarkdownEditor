namespace Frank.MarkdownEditor.Controls.Extensions;

public static class StringExtensions
{
    public static string ConformLineBreaks(this string text) => text.Replace("\r\n", "\n").Replace("\r", "\n");
}