using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.ComponentModel;

namespace Frank.MarkdownEditor.Controls.ViewComponents;

public interface ICodeContent : INotifyPropertyChanged
{
    string Content { get; set; }
    
    FileReference File { get; set; }
    
    IHighlightingDefinition HighlightingDefinition { get; set; }
    
    void Save();
    
    void Load();
    
    event EventHandler TextChanged;
}