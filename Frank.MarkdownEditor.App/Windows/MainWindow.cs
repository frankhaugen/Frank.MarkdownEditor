using Frank.MarkdownEditor.Controls.Contexts;
using Frank.MarkdownEditor.Controls.Extensions;
using Frank.MarkdownEditor.Controls.UserControls;
using System.ComponentModel;
using System.Windows;

namespace Frank.MarkdownEditor.App;

internal class MainWindow : Window
{
    private readonly ILogger<MainWindow> _logger;
    private readonly WindowContext _window;

    public MainWindow(ILogger<MainWindow> logger, MainGrid mainGrid, WindowContext window)
    {
        _logger = logger;
        _window = window;

        ConfigureWindow();
        
        Content = mainGrid;
    }

    private void ConfigureWindow()
    {
        MinWidth = 512;
        MinHeight = 256;

        Title = "Markdown Editor";

        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
        _window.Size = new Size(MinWidth, MinHeight);
        _window.Location = new Point(0, 0);
        _window.WindowState = WindowState.Normal;
        _window.Name = Name;
        _window.Title = Title;
        _window.ClassName = GetType().Name;

        var screen = this.GetScreen();
        
        _window.OwnerScreen = new ScreenContext()
        {
            Name = screen.DeviceName,
            DeviceName = screen.DeviceName,
            Primary = screen.Primary,
            Size = new Size(screen.Bounds.Width, screen.Bounds.Height),
            Location = new Point(screen.Bounds.X, screen.Bounds.Y)
        };
        
        _logger.LogInformation("Window configured");
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        _window.Size = sizeInfo.NewSize;
        base.OnRenderSizeChanged(sizeInfo);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        _logger.LogInformation("Closing");
        base.OnClosing(e);
    }
}