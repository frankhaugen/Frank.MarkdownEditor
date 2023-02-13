using Frank.MarkdownEditor.Controls.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Frank.MarkdownEditor.Controls.Contexts;

public class FileContext
{
    private static readonly DirectoryInfo Root = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "FrankMarkdownEditor"));
    private static DirectoryInfo FileDirectory => new(Path.Combine(Root.FullName, "Files"));
    private static DirectoryInfo DataDirectory => new(Path.Combine(Root.FullName, "Data"));
    private static FileInfo SettingsFile => new(Path.Combine(DataDirectory.FullName, "Settings.json"));
    
    private readonly ILogger<FileContext> _logger;

    public FileContext(ILogger<FileContext> logger)
    {
        _logger = logger;
        Setup();
    }

    private void Setup()
    {
        // Directories
        if (!Root.Exists) Root.Create();
        _logger.LogInformation($"Root directory: {Root.FullName}");
        if (!FileDirectory.Exists) FileDirectory.Create();
        _logger.LogInformation($"File directory: {FileDirectory.FullName}");
        if (!DataDirectory.Exists) DataDirectory.Create();
        _logger.LogInformation($"Data directory: {DataDirectory.FullName}");
        
        // Files
        if (!SettingsFile.Exists) SettingsFile.Create();
        _logger.LogInformation($"Settings file: {SettingsFile.FullName}");
        
        // Data
        foreach (var file in SetupFiles())
        {
            _logger.LogInformation($"Found file {file.DisplayName}");
            if (!file.IsCreated)
            {
                var fileHeader = $"# {file.DisplayName.Replace(".md", "")}";
                _logger.LogInformation($"Creating file {file.DisplayName} with header {fileHeader}");
                file.WriteAllTextAsync(fileHeader);
            }
        }
    }
    
    public FileMetadata Selected { get; private set; }
    
    public string Content { get; set; } = string.Empty;

    public event Action<FileMetadata> SelectedChanged;
    public event Action<FileMetadata> Saved;
    
    public async Task Select(FileMetadata file)
    {
        if (file is null || file == Selected) return;
        Selected = file;
        Content = await Selected.ReadAllTextAsync();
        SelectedChanged.Invoke(file);
    }
    
    public void Save()
    {
        if (Selected is not null)
        {
            Selected.WriteAllTextAsync(Content).GetAwaiter().GetResult();
            Saved.Invoke(Selected);
        }
    }
    
    public Dictionary<YearWeekDay, FileMetadata> GetDictionary() => DateTime.Today.GetYearWeeksDirectoryAndFiles(FileDirectory).CreateFilesForYearWithDictionary("md");
    
    public IEnumerable<FileMetadata> SetupFiles()
    {
        var setupContext = DateTime.Today.GetYearWeeksDirectoryAndFiles(FileDirectory);
        var files = setupContext.CreateFilesForYear("md");
        return files.Select(f => f.GetMetadata());
    }
    
    public async Task<Settings> GetSettingsAsync()
    {
        var json = await SettingsFile.ReadAllTextAsync();
        return Settings.FromJson(json);
    }
    
    public async Task SaveSettingsAsync(Settings settings)
    {
        var json = settings.ToJson();
        await SettingsFile.WriteAllTextAsync(json);
    }
}
