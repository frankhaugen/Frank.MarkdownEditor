using Frank.MarkdownEditor.Controls.UserControls;
using System.ComponentModel;
using System.Windows;

namespace Frank.MarkdownEditor.App;

internal class MainWindow : Window
{
    private readonly ILogger<MainWindow> _logger;
    private readonly MainGrid _mainGrid;

    public MainWindow(ILogger<MainWindow> logger, MainGrid mainGrid)
    {
        _logger = logger;
        _mainGrid = mainGrid;

        ConfigureWindow();
        
        _mainGrid.Width = Width;
        _mainGrid.Height = Height;
        
        Content = _mainGrid;
    }

    private void ConfigureWindow()
    {
        MinWidth = 512;
        MinHeight = 256;

        Title = "Markdown Editor";

        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        _logger.LogInformation("Closing");
        base.OnClosing(e);
    }
}