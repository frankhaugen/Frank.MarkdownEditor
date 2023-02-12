using System.Text.Json;

namespace Frank.MarkdownEditor.Controls.Contexts;

public class Settings
{
    public string LastOpenedFile { get; set; }
    
    public string ToJson() => JsonSerializer.Serialize(this, JsonOptions.FileDefault);

    public static Settings FromJson(string json) => JsonSerializer.Deserialize<Settings>(json, JsonOptions.FileDefault);
}