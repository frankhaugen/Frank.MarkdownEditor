using System;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Generators;

public interface IInputGenerator
{
    GroupBox GetTextInput(Guid id, string displayName, Action<object, TextChangedEventArgs> textChanged);
}