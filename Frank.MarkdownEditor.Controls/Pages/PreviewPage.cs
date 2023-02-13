using Frank.MarkdownEditor.Controls.Contexts;
using Markdig;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class PreviewPage : Page
{
    private readonly WebBrowser _browser = new();
    private readonly FileContext _fileContext;

    public PreviewPage(FileContext fileContext)
    {
        _fileContext = fileContext;
        _fileContext.SelectedChanged += FileContextOnSelectedChanged;   
        Content = _browser;
    }

    private void FileContextOnSelectedChanged(FileMetadata obj) => UpdatePreview(obj);

    public void UpdatePreview(FileMetadata file)
    {
        if (file is null) return;
        var markdown = file.ReadAllTextAsync().GetAwaiter().GetResult();
        var html = Markdown.ToHtml(markdown);
        _browser.NavigateToString(html);
    }

}