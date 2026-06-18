using System.Windows;
using System.Windows.Media.Imaging;
using StroyMaterials.App.Views;

namespace StroyMaterials.App;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DatabaseInitializer.EnsureDatabase();

        if (File.Exists(AppPaths.AppIcon))
        {
            var icon = BitmapFrame.Create(new Uri(AppPaths.AppIcon, UriKind.Absolute));
            Resources["AppIcon"] = icon;
        }

        if (e.Args.Length >= 2 && e.Args[0] == "--export-screenshots")
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            ScreenshotExporter.Export(e.Args[1]);
            Shutdown();
            return;
        }

        new LoginWindow().Show();
    }
}
