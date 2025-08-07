using Avalonia;
using Avalonia.ReactiveUI;
using System;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;

namespace Todo
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            IconProvider.Current
                .Register<FontAwesomeIconProvider>();
            var builder = AppBuilder.Configure<App>();
            builder = builder
                .UsePlatformDetect()
                .LogToTrace()
                // .WithIcons(container => container.Register<FontAwesomeIconProvider>())
                .UseReactiveUI();
            
            var x11Options = new X11PlatformOptions
            {
                RenderingMode = [X11RenderingMode.Vulkan, X11RenderingMode.Egl, X11RenderingMode.Glx, X11RenderingMode.Software],
                UseRetainedFramebuffer = true,
                OverlayPopups = true
            };

            if (OperatingSystem.IsFreeBSD())
            {
                builder = builder
                    .UseSkia()
                    .UseX11()
                    .With(x11Options);
            }
            else
            {
                builder = builder
                    .UsePlatformDetect()
                    .With(x11Options);
            }

            return builder;
        }
    }
}