using Frank.MarkdownEditor.Controls.Contexts;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class MarkdownPreviewPage : Page
{
    private readonly FileContext _fileContext;
    private readonly Markdig.Wpf.MarkdownViewer _viewer = new();

    public MarkdownPreviewPage(FileContext fileContext)
    {
        _fileContext = fileContext;
        _fileContext.SelectedChanged += FileContextOnSelectedChanged;
        _fileContext.Saved += FileContextOnSaved;
        _viewer.Markdown = "# Hello World";
        
        Content = _viewer;
    }

    private async void FileContextOnSaved(FileMetadata obj)
    {
        if (obj is null) return;
        UpdatePreview(obj);
    }

    private async void FileContextOnSelectedChanged(FileMetadata obj) => UpdatePreview(obj);

    public async void UpdatePreview(FileMetadata file)
    {
        if (file is null) return;
        var markdown = await file.ReadAllTextAsync();
        _viewer.Markdown = markdown;
    }
}