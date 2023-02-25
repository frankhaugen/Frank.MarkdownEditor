using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Frank.MarkdownEditor.Controls.ViewComponents;

public class DirectoryReference
{
    private readonly IEnumerable<string> _pathSegments;
    
    public DirectoryReference(IEnumerable<string> pathSegments)
    {
        if (pathSegments.SelectMany(x => x).Any(x => Path.GetInvalidPathChars().Contains(x)))
            throw new ArgumentException("Path contains invalid characters", nameof(pathSegments));
        
        _pathSegments = pathSegments;
    }
    
    public DirectoryReference(string path, string separator = "/")
    {
        if (path.Any(x => Path.GetInvalidPathChars().Contains(x)))
            throw new ArgumentException("Path contains invalid characters", nameof(path));

        _pathSegments = path.Split(separator, StringSplitOptions.RemoveEmptyEntries);
    }

    public bool Exists => Directory.Exists(ToString());
    
    public void Create() => Directory.CreateDirectory(ToString());

    public override string ToString() => Path.Combine(_pathSegments.ToArray());
    
    public FileReference GetFile(string name, string extension) => new FileReference(this, name, extension);
    
    public IEnumerable<FileReference> GetFiles(string pattern = "*") => Directory.EnumerateFiles(ToString(), pattern)
        .Select(x => new FileReference(this, Path.GetFileNameWithoutExtension(x), Path.GetExtension(x)));

    public IEnumerable<DirectoryReference> GetDirectories() => Directory.GetDirectories(ToString())
        .Select(x => new DirectoryReference(x));

    public DirectoryReference GetSubDirectory(string name) => new(_pathSegments.Append(name));
}