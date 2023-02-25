using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.ViewComponents;

public class MarkdownEditorControl : TabControl
{
    private readonly ICodeContent _codeContent;

    public MarkdownEditorControl(ICodeContent codeContent)
    {
        _codeContent = codeContent;

        Items.Add(new MarkdownPreview(_codeContent));
        Items.Add(new MarkdownEditor(_codeContent));
        
        SelectedIndex = 0;
    }
}