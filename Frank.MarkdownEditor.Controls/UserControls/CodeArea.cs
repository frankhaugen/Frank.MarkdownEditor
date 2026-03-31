using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.UserControls;

public class CodeArea : ICSharpCode.AvalonEdit.TextEditor
{
    public CodeArea(IHighlightingDefinition highlightingDefinition)
    {
        SyntaxHighlighting = highlightingDefinition;
        FontFamily = new("Consolas");
        FontSize = 12;
        ShowLineNumbers = true;
        WordWrap = true;
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
    }
}