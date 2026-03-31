using System;
using System.Windows;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.UserControls.Markdown;

public class ButtonBar : StackPanel
{
    public ButtonBar()
    {
        Orientation = Orientation.Horizontal;
        HorizontalAlignment = HorizontalAlignment.Right;
        VerticalAlignment = VerticalAlignment.Bottom;
        Margin = new Thickness(5);
    }
    
    public void AddButton(string text, Action<object, RoutedEventArgs> click)
    {
        var button = new Button { Content = text };
        button.Click += click.Invoke;
        Children.Add(button);
    }
    
    public void AddSeparator()
    {
        Children.Add(new Separator());
    }
}