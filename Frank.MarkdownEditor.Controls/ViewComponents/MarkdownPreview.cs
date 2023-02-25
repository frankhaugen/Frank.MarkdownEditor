using Markdig.Wpf;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.ViewComponents;

public class MarkdownPreview : TabItem
{
    private readonly MarkdownViewer _markdownViewer = new();
    private readonly ICodeContent _codeContent;
    
    public MarkdownPreview(ICodeContent codeContent)
    {
        _codeContent = codeContent;
        Header = "Preview";

        Content = _markdownViewer;
        Markdown = _codeContent.Content;
        
        _codeContent.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(ICodeContent.Content))
            {
                Markdown = _codeContent.Content;
            }
        };
        
        _codeContent.TextChanged += (sender, args) =>
        {
            Markdown = _codeContent.Content;
        };
    }

    public string Markdown
    {
        get => _markdownViewer.Markdown;
        set => _markdownViewer.Markdown = value;
    }
}