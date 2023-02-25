using Frank.MarkdownEditor.Controls.UserControls;
using ICSharpCode.AvalonEdit.Highlighting;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Frank.MarkdownEditor.Controls.ViewComponents;

public class MarkdownEditor : TabItem
{
    private readonly CodeArea _codeArea;
    private readonly ICodeContent _codeContent;
    
    public MarkdownEditor(ICodeContent codeContent)
    {
        _codeContent = codeContent;
        _codeArea = new CodeArea(HighlightingManager.Instance.GetDefinition("MarkDown"));
        
        Header = "Editor";
        
        Content = _codeArea;
        Markdown = _codeContent.Content;
        
        _codeArea.Text = Markdown;
        
        _codeContent.PropertyChanged += OnCodeContentOnPropertyChanged;
        _codeContent.TextChanged += (sender, args) => Markdown = _codeContent.Content;

        _codeArea.TextChanged += (sender, args) => Markdown = _codeArea.Text;
        _codeArea.TextChanged += (sender, args) => Markdown = _codeArea.Text;
        
        _codeArea.KeyDown += HandleSaveKeyDownEvent;
        // _codeArea.KeyDown += HandleTabKeyDownEvent;
    }

    private void OnCodeContentOnPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(ICodeContent.Content)) Markdown = _codeContent.Content;
    }

    private void HandleSaveKeyDownEvent(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) _codeContent.Save();
    }
    
    private void HandleTabKeyDownEvent(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Tab)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) ;
            
            _codeArea.TextArea.Document.Insert(_codeArea.TextArea.Caret.Offset, "    ");
            e.Handled = true;
        }
    }
    
    public string Markdown
    {
        get => _codeContent.Content;
        set => _codeContent.Content = value;
    }
}