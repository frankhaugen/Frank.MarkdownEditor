using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class RoslynPadPage : Page
{
    public RoslynPadPage() => Content = new RoslynPad.Editor.RoslynCodeEditor() { };
}