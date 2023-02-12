using Frank.MarkdownEditor.Controls.Components;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class ScriptingPage : Page
{
    public ScriptingPage(IScriptingComponent scriptingComponent)
    {
        
        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
        VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
        Content = scriptingComponent.GetPanel();
    }
}