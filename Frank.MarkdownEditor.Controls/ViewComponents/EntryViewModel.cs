using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Frank.MarkdownEditor.Controls.ViewComponents;

public class EntryViewModel : ICodeContent
{
    private string _content;
    
    public EntryViewModel(FileReference file)
    {
        File = file;
        HighlightingDefinition = HighlightingManager.Instance.GetDefinitionByExtension(file.Extension);
        Load();
    }

    public string Content
    {
        get => _content;
        set
        {
            if (value == _content) return;
            _content = value;
            OnPropertyChanged();
        }
    }

    public FileReference File { get; set; }

    public IHighlightingDefinition HighlightingDefinition { get; set; }

    public void Save() => File.WriteAllText(Content);
    public void Load() => Content = File.ReadAllText();

    public event EventHandler? TextChanged;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}