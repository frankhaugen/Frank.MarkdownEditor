using Frank.MarkdownEditor.Controls.Components;
using Frank.MarkdownEditor.Controls.Extensions;
using Frank.MarkdownEditor.Controls.Pages;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.UserControls;

public class MainGrid : Grid
{
    private readonly GroupBox _treeGroupBox;
    private readonly GroupBox _editorGroupBox;
    private readonly GroupBox _priviewGroupBox;
    
    public MainGrid()
    {
        this.GenerateGridRowsAndColumns(1, 3);
        
        _treeGroupBox = new GroupBox()
        {
            Name = "Tree",
            Header = "Files",
            Background = System.Windows.Media.Brushes.Beige
        };
        _editorGroupBox = new GroupBox()
        {
            Name = "Editor",
            Header = "Editor",
            Background = System.Windows.Media.Brushes.Beige
        };
        _priviewGroupBox = new GroupBox()
        {
            Name = "Preview",
            Header = "Preview",
            Background = System.Windows.Media.Brushes.Beige
        };
        
        this.AddChild(_treeGroupBox, 0, 0);
        this.AddChild(_editorGroupBox, 0, 1);
        this.AddChild(_priviewGroupBox, 0, 2);

        _editorGroupBox.Content = new Frame()
        {
            Content = new ScriptingPage(new ScriptingComponent())
        };
        
        
    }

}