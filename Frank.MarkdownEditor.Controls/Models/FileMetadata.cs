using Frank.MarkdownEditor.Controls.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Frank.MarkdownEditor.Controls.Contexts;

public class FileMetadata
{
    private readonly FileInfo _file;
    
    public FileMetadata(FileInfo file) => _file = file;

    public string DisplayName => _file.Name;
    public bool IsHidden => _file.Name.StartsWith(".") || !_file.Name.EndsWith(".md");
    public bool IsCreated => _file.Exists;
    
    public int Week => ISOWeek.GetWeekOfYear(_file.CreationTime);
    public int Year => _file.CreationTime.Year;
    public DayOfWeek Day => _file.CreationTime.DayOfWeek;
    
    public async Task<IEnumerable<string>> ReadAllLinesAsync() => await _file.ReadAllLinesAsync();
    public async Task<string> ReadAllTextAsync() => await _file.ReadAllTextAsync();
    public async Task WriteAllTextAsync(string text) => await _file.WriteAllTextAsync(text);
    public async Task AppendAllTextAsync(string text) => await _file.AppendAllTextAsync(text);
    
    public async Task<bool> HasHeadingAsync()
    {
        var lines = await ReadAllLinesAsync();
        return lines.Any() && lines.First().StartsWith("#");
    }
    
    public async Task<string> GetHeadingAsync()
    {
        var lines = await ReadAllLinesAsync();
        return lines.Any() && lines.First().StartsWith("#") ? lines.First() : string.Empty;
    }
    
    public async Task SetHeadingAsync(string heading)
    {
        var lines = await ReadAllLinesAsync();
        var listOfLines = lines.ToList();
        var headingExists = listOfLines.Any() && listOfLines.First().StartsWith("#");

        if (headingExists)
            listOfLines[0] = heading;
        else
            listOfLines.Insert(0, heading);
        
        await WriteAllTextAsync(string.Join(Environment.NewLine, listOfLines));
    }
    
}