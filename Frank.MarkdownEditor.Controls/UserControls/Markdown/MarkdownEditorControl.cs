using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.Highlighting;

namespace Frank.MarkdownEditor.Controls.UserControls.Markdown;

public class MarkdownEditorControl : UserControl
{
    private readonly Grid _grid;
    private readonly CodeArea _codeArea;
    private readonly WebBrowser _viewer;
    private readonly ButtonBar _toolbar;
    private readonly FileInfo _file;
 
    public MarkdownEditorControl(FileInfo file)
    {
        _file = file;
        
        if (!file.Exists) File.WriteAllText(file.FullName, "");
        if (file.Extension != ".md") throw new ArgumentException("File must be a markdown file", nameof(file));

        _grid = new Grid();
        _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
        _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
        _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        
        var gridSplitter = new GridSplitter
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            Width = 5,
            Background = SystemColors.ControlDarkBrush,
            ResizeDirection = GridResizeDirection.Columns,
            ResizeBehavior = GridResizeBehavior.PreviousAndNext
        };

        foreach (var definition in HighlightingManager.Instance.HighlightingDefinitions)  
        {
            Console.WriteLine(definition.Name);
        }
        
        _codeArea = new CodeArea(HighlightingManager.Instance.GetDefinition("MarkDownWithFontSize"));
        _viewer = new WebBrowser();
        _toolbar = new ButtonBar();
        
        _codeArea.Text = File.ReadAllText(file.FullName);
        _codeArea.TextChanged += (sender, args) =>
        {
            _viewer.NavigateToString(Markdig.Markdown.ToHtml(_codeArea.Text));
        };
        _codeArea.KeyDown += (sender, args) =>
        {
            if (args.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) Save();
        };
        
        _viewer.NavigateToString(Markdig.Markdown.ToHtml(_codeArea.Text));
        
        _toolbar.AddButton("Save", (sender, args) => Save());
        
        Grid.SetRow(_toolbar, 1);
        Grid.SetRow(_codeArea, 0);
        Grid.SetRow(_viewer, 0);
        
        Grid.SetColumn(_toolbar, 0);
        Grid.SetColumn(_codeArea, 0);
        Grid.SetColumn(gridSplitter, 1);
        Grid.SetColumn(_viewer, 2);
        
        _grid.Children.Add(_toolbar);
        _grid.Children.Add(_codeArea);
        _grid.Children.Add(_viewer);
        _grid.Children.Add(gridSplitter);
        
        _grid.Width = 1200;
        _grid.Height = 800;
        
        Content = _grid;
    }

    private void Save()
    {
        File.WriteAllText(_file.FullName, _codeArea.Text);
    }
}