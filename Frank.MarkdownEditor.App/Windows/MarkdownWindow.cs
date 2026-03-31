using System.Windows;
using Frank.MarkdownEditor.Controls.UserControls.Markdown;
using Microsoft.Win32;

namespace Frank.MarkdownEditor.App.Windows;

public class MarkdownWindow : Window
{
    public MarkdownWindow()
    {
        Title = "Markdown Editor";
        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        WindowStyle = WindowStyle.ToolWindow;
        ShowActivated = true;
        // MaxHeight = SystemParameters.WorkArea.Height - SystemParameters.WorkArea.Height / 10;
        // MaxWidth = SystemParameters.WorkArea.Width - SystemParameters.WorkArea.Width / 10;
        
        var fileDialog = new OpenFileDialog()
        {
            Title = "Open Markdown File",
            Filter = "Markdown Files (*.md)|*.md",
            Multiselect = false,
            CheckFileExists = true,
            CheckPathExists = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
        };
        
        var fileDialogResult = fileDialog.ShowDialog();
        
        if (fileDialogResult == null || !fileDialogResult.Value)
        {
            Close();
            return;
        }
        
        var filePath = fileDialog.FileName;
        var file = new FileInfo(filePath);
        
        var markdownEditorComponent = new MarkdownEditorControl(file);
        Content = markdownEditorComponent;
    }
}