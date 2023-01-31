using Frank.MarkdownEditor.Controls.Extensions;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Components;

public class ScriptingComponent : IScriptingComponent
{
    private StackPanel _panel = new();
    private TextEditor _editor = new();
    private Button _button = new();

    private ScriptOptions _scriptOptions;
    private object _globals;

    public ScriptingComponent()
    {
        ConfigureEditor();
        _scriptOptions = CreateScriptOptions();

        _button.Content = "Run";
        _button.Click += ButtonOnClick;

        _panel.Children.Add(_button);
        _panel.Children.Add(_editor);
    }

    private ScriptOptions CreateScriptOptions() =>
        ScriptOptions.Default
            .AddReferences(GetType().Assembly)
            .AddImports(
                "System.Text.Json",
                "Company.IntegrationEngine.Plugin.Models.Invoices"
            );

    private async void ButtonOnClick(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;

        try
        {
            var result = await RunCodeAsync();
            MessageBox.Show(result);
        }
        catch (CompilationErrorException exception)
        {
            MessageBox.Show(string.Join(Environment.NewLine, exception.Diagnostics), nameof(CompilationErrorException).ToSentenceCase(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }

    private async Task<string> RunCodeAsync()
    {
        var result = await CSharpScript.RunAsync(_editor.Document.Text, _scriptOptions, _globals);
        return Convert.ToString(result.ReturnValue) ?? string.Empty;
    }

    private async Task<string> EvaluateCodeAsync()
    {
        var result = await CSharpScript.EvaluateAsync(_editor.Document.Text, _scriptOptions, _globals);

        return Convert.ToString(result) ?? string.Empty;
    }

    private void ConfigureEditor()
    {
        _editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(".cs");

        _editor.Document = new TextDocument();
        _editor.Encoding = Encoding.UTF8;
        _editor.ShowLineNumbers = true;
    }

    public StackPanel GetPanel() => _panel;
}