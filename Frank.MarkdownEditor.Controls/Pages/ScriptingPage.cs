using Frank.MarkdownEditor.Controls.Components;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class ScriptingPage : Page
{
    public ScriptingPage(IScriptingComponent scriptingComponent) => Content = scriptingComponent.GetPanel();
}