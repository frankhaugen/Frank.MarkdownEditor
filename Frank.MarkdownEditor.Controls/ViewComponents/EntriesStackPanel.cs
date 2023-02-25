using Frank.MarkdownEditor.Controls.UserControls;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.ViewComponents;

public class EntriesStackPanel : StackPanel
{
    private readonly IEnumerable<EntryViewModel> _entries;
    private readonly Button _addButton;
    
    public EntriesStackPanel(IEnumerable<EntryViewModel> entries, DirectoryReference dataDirectory)
    {
        _entries = entries;
        _addButton = new()
        {
            Content = "Add",
            Margin = new(0, 0, 0, 10)
        };
        
        _addButton.Click += (sender, args) =>
        {
            var dialogContext = new TextDialogContext();
            dialogContext.Text = $"{DateTime.UtcNow:yyyyMMddTHHmm}";
            var dialog = new TextInputDialog(dialogContext);
            dialog.ShowDialog();
            
            var file = new FileReference(dataDirectory, dialogContext.Text, "md");
            file.WriteAllText("# New Entry");
            var entry = new EntryViewModel(file);
            var entryView = new EntryView(entry);
            
            Children.Insert(Children.Count - 1, entryView);
        };
        
        foreach (var entry in _entries)
        {
            var entryView = new EntryView(entry);
            
            Children.Add(entryView);
        }
        
        Children.Add(_addButton);
        
    }
}