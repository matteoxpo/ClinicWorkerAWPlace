using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using Avalonia;
using Avalonia.ReactiveUI;
using Data.Repositories;
using Domain.Entities;
using Domain.Entities.People;
using Presentation.Configuration;

namespace Presentation;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
    }
}