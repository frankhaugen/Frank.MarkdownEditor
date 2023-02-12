using Frank.MarkdownEditor.App.Extensions;
using Frank.MarkdownEditor.Controls.Contexts;
using Frank.MarkdownEditor.Controls.Pages;
using Frank.MarkdownEditor.Controls.UserControls;
using System.Runtime.InteropServices;
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

                services.AddScoped<Application>();
                
                services.AddScoped<MainGrid>();
                services.AddScoped<FileContext>();
                
                services.AddScoped<TreePage>();
                services.AddScoped<PreviewPage>();
                services.AddScoped<RoslynPadPage>();

                services.AddScoped<MainWindow>();
                services.AddHostedService<WindowHost>();
            })
            .Build();

        host.Run();
    }

    [DllImport("kernel32")]
    static extern bool AllocConsole();
}