using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.ViewComponents;

public class EntryHeader : Expander
{
    public EntryHeader(EntryViewModel entryViewModel)
    {
        Header = entryViewModel.HighlightingDefinition.Name;
        Content = new MarkdownEditorControl(entryViewModel);
    }
}