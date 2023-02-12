using Frank.MarkdownEditor.Controls.Extensions;
using Frank.MarkdownEditor.Controls.Pages;
using Frank.MarkdownEditor.Controls.UserControls;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Windows;

namespace Frank.MarkdownEditor.Controls.Windows;

public class MainWindow : Window
{
    private readonly ILogger<MainWindow> _logger;
    private readonly CellGrid _grid;

    public MainWindow(ILogger<MainWindow> logger, InvoicePreviewPage invoicePreviewPage)
    {
        _logger = logger;

        _grid = new CellGrid(3, 3);

        _grid.SetCellContent(0, 1, invoicePreviewPage);

        ConfigureWindow();

        Content = _grid;
    }

    private void ConfigureWindow()
    {
        var size = this.GetScreenWorkingAreaSize();
        MinWidth = 256;
        MinHeight = 128;
        MaxHeight = size.Height;
        MaxWidth = size.Width;

        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        _logger.LogInformation("Closing");
        base.OnClosing(e);
    }
}