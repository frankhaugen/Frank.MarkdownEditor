using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Frank.MarkdownEditor.Controls.Contexts;

public class YearWeekDayJsonConverter : JsonConverter<YearWeekDay>
{
    public override YearWeekDay Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return YearWeekDay.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, YearWeekDay value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}