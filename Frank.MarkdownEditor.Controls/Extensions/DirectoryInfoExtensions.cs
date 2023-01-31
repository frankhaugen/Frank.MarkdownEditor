using System.IO;

namespace Frank.MarkdownEditor.Controls.Extensions;

public static class DirectoryInfoExtensions
{
    public static DirectoryInfo EnsureExist(this DirectoryInfo directoryInfo)
    {
        if (directoryInfo.Exists) return directoryInfo;
        throw new DirectoryNotFoundException($"Directory '{directoryInfo.FullName}' does not exist");
    }
}