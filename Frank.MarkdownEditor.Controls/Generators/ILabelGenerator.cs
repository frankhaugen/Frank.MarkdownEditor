using System;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Generators;

public interface ILabelGenerator
{
    GroupBox GetLabel(Guid id, string displayName, string value);
}