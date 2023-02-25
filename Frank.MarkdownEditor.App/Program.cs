using Frank.MarkdownEditor.App.Extensions;
using Frank.MarkdownEditor.Controls.Contexts;
using Frank.MarkdownEditor.Controls.Models;
using Frank.MarkdownEditor.Controls.Pages;
using Frank.MarkdownEditor.Controls.UserControls;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Windows;

namespace Frank.MarkdownEditor.App;

internal class Program
{
    [STAThread]
    public static void Main(params string[] args)
    {
        AllocConsole();

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                context.SetContentPathToApplicationDirectory();

                var window = new WindowContext
                {
                    Name = "Markdown Editor",
                    Title = "Markdown Editor",
                    ClassName = "MarkdownEditor",
                    Size = new Size(800, 600),
                    Location = new Point(0, 0),
                    WindowState = WindowState.Normal,
                    OwnerScreen = new ScreenContext
                    {
                        Name = "Screen 1",
                        DeviceName = "\\\\.\\DISPLAY1",
                        Primary = true,
                        Size = new Size(1920, 1080),
                        Location = new Point(0, 0)
                    }
                };

                services.Configure<Setup>(context.Configuration.GetSection(nameof(Setup)));
                
                services.AddSingleton<WindowContext>(window);
                
                services.AddScoped<Application>();
                
                services.AddScoped<MainGrid>();
                services.AddScoped<FileContext>();
                
                services.AddScoped<TreePage>();
                services.AddScoped<MarkdownPreviewPage>();
                services.AddScoped<RoslynPadPage>();

                services.AddScoped<MainWindow>();
                services.AddScoped<LogWindow>();
                services.AddHostedService<WindowHost>();
            })
            .Build();

        host.Run();
    }

    [DllImport("kernel32")]
    static extern bool AllocConsole();
}