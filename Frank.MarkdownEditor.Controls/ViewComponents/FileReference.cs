using System;
using System.IO;
using System.Linq;

namespace Frank.MarkdownEditor.Controls.ViewComponents;

public class FileReference
{
    public string Name { get; set; }
    public DirectoryReference Directory { get; set; }
    public string Extension { get; set; }
    
    public FileReference(DirectoryReference directory, string name, string extension)
    {
        Directory = directory;
        Name = name;
        Extension = extension.TrimStart('.');
    }

    public override string ToString() => Path.Combine(Directory.ToString(), $"{Name}.{Extension}");
    public string ReadAllText() => File.ReadAllText(ToString());
    public void WriteAllText(string text) => File.WriteAllText(ToString(), text);
}