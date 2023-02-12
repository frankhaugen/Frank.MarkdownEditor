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
            
        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
        VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
        Content = _roslynPadControl;
    }

    private void FileContextOnSelectedChanged(FileMetadata obj)
    {
        CurrentFile = obj;
        _roslynPadControl.Text = CurrentFile.ReadAllTextAsync().GetAwaiter().GetResult();
    }
    
    public FileMetadata CurrentFile { get; private set; }
}