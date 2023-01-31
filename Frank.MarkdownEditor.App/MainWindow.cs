using Frank.MarkdownEditor.Controls.UserControls;
using System.ComponentModel;
using System.Windows;

namespace Frank.MarkdownEditor.App;

internal class MainWindow : Window
{
    private readonly ILogger<MainWindow> _logger;
    private readonly MainGrid _mainGrid = new();

    public MainWindow(ILogger<MainWindow> logger)
    {
        _logger = logger;

        ConfigureWindow();

        Content = _mainGrid;
    }

    private void ConfigureWindow()
    {
        MinWidth = 512;
        MinHeight = 256;

        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        _logger.LogInformation("Closing");
        base.OnClosing(e);
    }
}