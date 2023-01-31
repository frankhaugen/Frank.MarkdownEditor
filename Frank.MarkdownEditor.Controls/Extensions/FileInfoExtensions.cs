using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Frank.MarkdownEditor.Controls.Extensions;

public static class FileInfoExtensions
{
    public static FileInfo EnsureExist(this FileInfo file)
    {
        if (file.Exists) return file;
        throw new FileNotFoundException($"File '{file.FullName}' does not exist", file.Name);
    }

    public static void Create(this FileInfo file, string text = "")
    {
        if (file.Exists) return;
        using var writer = file.CreateText();
        writer.Write(text);
    }

    public static void Create(this FileInfo file, byte[] data, bool force = false)
    {
        if (file.Exists && !force) return;
        using var writer = file.OpenWrite();
        writer.Write(data);
    }

    public static string Read(this FileInfo file)
    {
        var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var streamReader = new StreamReader(fileStream);
        var text = streamReader.ReadToEnd();
        streamReader.Close();
        return text;
    }

    public static byte[] ReadRaw(this FileInfo file)
    {
        var stream = new MemoryStream();
        var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        fileStream.CopyTo(stream);
        return stream.ToArray();
    }

    public static T? ReadJson<T>(this FileInfo file) where T : class
    {
        using var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read);
        return JsonSerializer.DeserializeAsync<T>(stream).GetAwaiter().GetResult();

        using var memoryStream = new MemoryStream();
        using var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
        fileStream.CopyTo(memoryStream);
        var result = JsonSerializer.Deserialize<T>(memoryStream.ToArray(), new JsonSerializerOptions()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        });

        fileStream.Close();
        return result;
    }

    public static T? ReadNewtonsoftJson<T>(this FileInfo file) where T : class
    {
        var text = file.Read();
        var result = JsonSerializer.Deserialize<T>(text);
        return result;
    }

    public static List<string> ReadLines(this FileInfo file)
    {
        var text = file.Read();
        var cleanedText = text.ConformLineBreaks();
        var lines = new List<string>(cleanedText.Split('\n'));
        return lines;
    }

    public static void WriteJson<T>(this FileInfo file, T value) where T : class, new()
    {
        file.CreateJson<T>();
        var json = JsonSerializer.Serialize(value, new JsonSerializerOptions() { WriteIndented = true });
        file.Create(json);
    }

    public static void CreateJson<T>(this FileInfo file) where T : class, new()
    {
        if (file.Exists) return;
        var body = new T();
        var text = JsonSerializer.Serialize(body, new JsonSerializerOptions() { WriteIndented = true });
        using var writer = file.CreateText();
        writer.Write(text);
    }
}