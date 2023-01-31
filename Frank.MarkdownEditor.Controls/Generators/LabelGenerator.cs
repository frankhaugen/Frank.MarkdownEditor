using Frank.MarkdownEditor.Controls.Extensions;
using System;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Generators;

public class LabelGenerator : ILabelGenerator
{
    public GroupBox GetLabel(Guid id, string displayName, string value)
    {
        var groupBox = new GroupBox() { Header = displayName };

        var label = new Label() { };
        label.SetId(id);
        label.Content = value;

        groupBox.Content = label;

        return groupBox;
    }
}