using System.Net.Mail;
using Frank.MarkdownEditor.App.Extensions;
using Frank.MarkdownEditor.Controls.Contexts;
using Frank.MarkdownEditor.Controls.Models;
using Frank.MarkdownEditor.Controls.Pages;
using Frank.MarkdownEditor.Controls.UserControls;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Windows;
using Frank.MarkdownEditor.App.Windows;
using Frank.MarkdownEditor.Core.Git;

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
                    // Name = "Markdown Editor",
                    // Title = "Markdown Editor",
                    // ClassName = "MarkdownEditor",
                    // Size = new Size(800, 600),
                    // Location = new Point(0, 0),
                    // WindowState = WindowState.Normal,
                    // OwnerScreen = new ScreenContext
                    // {
                    //     Name = "Screen 1",
                    //     DeviceName = "\\\\.\\DISPLAY1",
                    //     Primary = true,
                        // Size = new Size(1920, 1080),
                        // Location = new Point(0, 0)
                    // }
                };

                services.Configure<Setup>(context.Configuration.GetSection(nameof(Setup)));
                services.Configure<GitRepositoryConfiguration>(x =>
                {
                    x.RepositoryName = "Frank.Notes";
                    x.Password = Environment.GetEnvironmentVariable("GITHUB_TOKEN") ?? string.Empty;
                    x.Username = new MailAddress("frank.haugen@gmail.com");
                    x.Uri = new Uri("https://github.com/frankhaugen/Frank.Notes.git");
                    x.WorkDirectory = new DirectoryInfo("D:/temp/repo");
                });
                
                services.AddSingleton<WindowContext>(window);
                
                services.AddScoped<Application>();
                services.AddScoped<IGitRepositoryService, GitRepositoryService>();
                
                services.AddScoped<MainGrid>();
                services.AddScoped<FileContext>();
                
                services.AddScoped<TreePage>();
                services.AddScoped<MarkdownPreviewPage>();
                services.AddScoped<RoslynPadPage>();

                services.AddScoped<MarkdownWindow>();
                services.AddScoped<MainWindow>();
                services.AddScoped<LogWindow>();
                services.AddScoped<GitWindow>();
                services.AddHostedService<WindowHost>();
            })
            .Build();

        host.Run();
    }

    [DllImport("kernel32")]
    static extern bool AllocConsole();
}