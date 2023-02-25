﻿using System.Runtime.InteropServices;
using System.Windows;

namespace Frank.MarkdownEditor.App;

internal class WindowHost : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WindowHost> _logger;

    public WindowHost(IServiceProvider serviceProvider, ILogger<WindowHost> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting");

        using var scope = _serviceProvider.CreateScope();
        var window = scope.ServiceProvider.GetRequiredService<LogWindow>();
        // var window = scope.ServiceProvider.GetRequiredService<MainWindow>();
        var app = scope.ServiceProvider.GetRequiredService<Application>();

        app.ShutdownMode = ShutdownMode.OnMainWindowClose;
        app.Exit += (sender, args) =>
        {
            FreeConsole();
            app.Shutdown();
            _logger.LogInformation("Stopping");
            Environment.Exit(0);
        };

        app.Run(window);
    }

    [DllImport("kernel32")]
    private static extern bool FreeConsole();
}