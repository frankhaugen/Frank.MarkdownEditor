using System.Text.Json;
using System.Text.Json.Serialization;

namespace Frank.MarkdownEditor.Core.Serialization.Converters;

public class JsonDirectoryInfoConverter : JsonConverter<DirectoryInfo>
{
    public override DirectoryInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string path = reader.GetString();
        return new DirectoryInfo(Path.GetFullPath(path));
    }

    public override void Write(Utf8JsonWriter writer, DirectoryInfo value, JsonSerializerOptions options)
    {
        // Convert to a Unix-like path format (which also works on Windows)
        string normalizedPath = value.FullName.Replace("\\", "/");
        writer.WriteStringValue(normalizedPath);
    }
}