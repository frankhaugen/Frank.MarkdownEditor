using Frank.MarkdownEditor.Controls.Contexts;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class RoslynPadPage : Page
{
    private readonly FileContext _fileContext;
    private readonly RoslynPad.Editor.RoslynCodeEditor _roslynPadControl;
    
    public RoslynPadPage(FileContext fileContext)
    {
        _fileContext = fileContext;

        _roslynPadControl = new RoslynPad.Editor.RoslynCodeEditor() { };
        
        _fileContext.SelectedChanged += FileContextOnSelectedChanged;
        _roslynPadControl.TextChanged += RoslynPadControlOnTextChanged;
            
        Content = _roslynPadControl;
    }

    private async void RoslynPadControlOnTextChanged(object? sender, EventArgs e)
    {
        if (CurrentFile is null) return;
        _fileContext.Content = _roslynPadControl.Text;
        await Save();
    }

    private async void FileContextOnSelectedChanged(FileMetadata obj)
    {
        if (obj is null) return;
        CurrentFile = obj;
        _roslynPadControl.Text = _fileContext.Content;
    }
    
    public FileMetadata CurrentFile { get; private set; }
    
    public async Task Save()
    {
        if (CurrentFile is null) return;
        await _fileContext.Save();
    }
}
