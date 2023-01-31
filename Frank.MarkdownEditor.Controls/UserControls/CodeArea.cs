using ICSharpCode.AvalonEdit.Highlighting;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.UserControls;

public class CodeArea : GroupBox
{
    private readonly ICSharpCode.AvalonEdit.TextEditor _content = new();

    public CodeArea(string header, string text = "")
    {
        Header = header;
        _content.Text = text;
        _content.IsReadOnly = true;
        _content.ShowLineNumbers = true;
        _content.WordWrap = true;
        _content.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        _content.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("Json");

        SizeChanged += (sender, args) => _content.Width = Width;

        base.Content = _content;
    }

    public new string Content
    {
        get => _content.Text as string ?? string.Empty;
        set => _content.Text = value;
    }
}