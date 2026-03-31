namespace Frank.MarkdownEditor.Core.Extensions;

public static class FileInfoExtensions
{
    public static bool IsCreated(this FileInfo file) => file.Length > 0;
    public static async Task<string> ReadAllTextAsync(this FileInfo file) => await File.ReadAllTextAsync(file.FullName);
    public static async Task WriteAllTextAsync(this FileInfo file, string content) => await File.WriteAllTextAsync(file.FullName, content);
	
    // Sync methods
    public static string ReadAllText(this FileInfo file) => File.ReadAllText(file.FullName);
    public static void WriteAllText(this FileInfo file, string content) => File.WriteAllText(file.FullName, content);
}