using Frank.MarkdownEditor.Controls.Contexts;
using Frank.MarkdownEditor.Controls.Extensions;
using Frank.MarkdownEditor.Controls.Pages;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.UserControls;

public class MainGrid : Grid
{
    private readonly WindowContext _window;
    
    public MainGrid(TreePage treePage, RoslynPadPage roslynPadPage, MarkdownPreviewPage previewPage, WindowContext window)
    {
        _window = window;
        this.GenerateGridRowsAndColumns(1, 3);

        this.AddPage(treePage, 0, 0);
        this.AddPage(roslynPadPage, 0, 1);
        this.AddPage(previewPage, 0, 2);

        MaxHeight = _window.OwnerScreen.Size.Height;
        MaxWidth = _window.OwnerScreen.Size.Width;
    }
}