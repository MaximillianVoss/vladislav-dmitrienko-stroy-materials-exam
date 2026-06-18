using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using StroyMaterials.App.Models;
using StroyMaterials.App.Views;

namespace StroyMaterials.App;

internal static class ScreenshotExporter
{
    public static void Export(string outputDirectory)
    {
        Directory.CreateDirectory(outputDirectory);

        Capture(new LoginWindow(), Path.Combine(outputDirectory, "01_login.png"));
        Capture(new MainWindow(UserSession.Guest), Path.Combine(outputDirectory, "02_guest_products.png"));
        Capture(new MainWindow(new UserSession
        {
            UserId = 1,
            FullName = "Ворсин Петр Евгеньевич",
            RoleName = "Администратор"
        }), Path.Combine(outputDirectory, "03_admin_products.png"));
        Capture(new OrderListWindow(new UserSession
        {
            UserId = 1,
            FullName = "Ворсин Петр Евгеньевич",
            RoleName = "Администратор"
        }), Path.Combine(outputDirectory, "04_orders.png"));
    }

    private static void Capture(Window window, string path)
    {
        window.WindowStartupLocation = WindowStartupLocation.Manual;
        window.Left = -32000;
        window.Top = -32000;
        window.ShowInTaskbar = false;
        window.Show();
        window.UpdateLayout();
        window.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.ApplicationIdle);

        var width = Math.Max(1, (int)Math.Ceiling(window.ActualWidth));
        var height = Math.Max(1, (int)Math.Ceiling(window.ActualHeight));
        var bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
        bitmap.Render(window);

        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmap));
        using var stream = File.Create(path);
        encoder.Save(stream);

        window.Close();
    }
}
