using Frank.MarkdownEditor.Controls.Extensions;
using System;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Generators;

public class InputGenerator : IInputGenerator
{
    public GroupBox GetTextInput(Guid id, string displayName, Action<object, TextChangedEventArgs> textChanged)
    {
        var groupBox = new GroupBox() { Header = displayName };

        var textBox = new TextBox() { };
        textBox.SetId(id);
        textBox.TextChanged += textChanged.Invoke;

        groupBox.Content = textBox;

        return groupBox;
    }
}