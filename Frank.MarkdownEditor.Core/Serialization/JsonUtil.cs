using System.Text.Json;
using System.Text.Json.Serialization;
using Frank.MarkdownEditor.Core.Serialization.Converters;

namespace Frank.MarkdownEditor.Core.Serialization;

public static class JsonUtil
{
    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, Options);
    }

    public static string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }

    public static JsonSerializerOptions Options = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true,
        Converters = {
            new JsonStringEnumConverter(),
            new JsonDirectoryInfoConverter(),
            new JsonMailAddressConverter()
        },
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };
}