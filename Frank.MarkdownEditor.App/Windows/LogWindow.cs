using Frank.MarkdownEditor.Controls.Models;
using Frank.MarkdownEditor.Controls.ViewComponents;
using Microsoft.Extensions.Options;
using System.Windows;

namespace Frank.MarkdownEditor.App;

internal class LogWindow : Window
{
    private readonly ContentManager _contentManager;
    private readonly ILogger<LogWindow> _logger;
    private readonly IOptions<Setup> _setup;
    
    public LogWindow(ILogger<LogWindow> logger, IOptions<Setup> setup)
    {
        _logger = logger;
        _setup = setup;

        _logger.LogInformation(_setup.Value.BasePath);
        
        _contentManager = new ContentManager(new DirectoryReference(_setup.Value.BasePath));
        
        var entries = _contentManager.GetEntries();
        
        Content = new EntriesStackPanel(entries, _contentManager.DataDirectory);
    }
}

public class ContentManager
{
    public ContentManager(DirectoryReference directoryReference)
    {
        DataDirectory = directoryReference.GetSubDirectory("Data");
        if (!DataDirectory.Exists) DataDirectory.Create();
    }
    
    public DirectoryReference DataDirectory { get; }

    public IEnumerable<EntryViewModel> GetEntries() => DataDirectory.GetFiles().Select(file => new EntryViewModel(file)).ToList();
}