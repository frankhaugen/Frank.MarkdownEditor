using System.Text.Json;

namespace Frank.MarkdownEditor.Controls.Contexts;

public static class JsonOptions
{
    public static JsonSerializerOptions FileDefault => new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };
}