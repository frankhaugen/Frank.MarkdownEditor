using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Extensions;

public static class StringExtensionscc
{
    public static Label ToLabel(this string value) => new() { Content = value };
}