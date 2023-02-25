using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.ViewComponents;

public class EntryView : UserControl
{
    private readonly Expander _expander;
    
    public EntryView(EntryViewModel entryViewModel)
    {
        _expander = new Expander();

        var entryTabControl = new MarkdownEditorControl(entryViewModel);
        
        _expander.Header = entryViewModel.File.Name;
        _expander.Content = entryTabControl;
        
        Content = _expander;
    }
}