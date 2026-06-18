using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StroyMaterials.App;

internal static class ImageTools
{
    public static ImageSource LoadProductImage(string relativePath, int decodePixelWidth = 120)
    {
        var path = Path.Combine(AppPaths.ProductImagesDirectory, relativePath);
        if (!File.Exists(path))
        {
            path = AppPaths.PlaceholderImage;
        }

        return LoadBitmap(path, decodePixelWidth);
    }

    public static ImageSource LoadLogo(int decodePixelWidth = 96)
    {
        return LoadBitmap(File.Exists(AppPaths.Logo) ? AppPaths.Logo : AppPaths.PlaceholderImage, decodePixelWidth);
    }

    public static string SaveProductImage(string sourcePath, string article)
    {
        Directory.CreateDirectory(AppPaths.ProductImagesDirectory);
        var fileName = $"{article}_{DateTime.Now:yyyyMMddHHmmss}.png";
        var targetPath = Path.Combine(AppPaths.ProductImagesDirectory, fileName);

        var source = LoadBitmap(sourcePath, 600);
        var resized = RenderImage(source, 300, 200);
        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(resized));
        using var stream = File.Create(targetPath);
        encoder.Save(stream);

        return fileName;
    }

    public static void DeleteProductImageIfCustom(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath) || relativePath == "picture.png")
        {
            return;
        }

        var path = Path.Combine(AppPaths.ProductImagesDirectory, relativePath);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static BitmapImage LoadBitmap(string path, int decodePixelWidth)
    {
        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.DecodePixelWidth = decodePixelWidth;
        bitmap.UriSource = new Uri(path, UriKind.Absolute);
        bitmap.EndInit();
        bitmap.Freeze();
        return bitmap;
    }

    private static RenderTargetBitmap RenderImage(ImageSource source, int width, int height)
    {
        var visual = new DrawingVisual();
        using (var context = visual.RenderOpen())
        {
            context.DrawRectangle(Brushes.White, null, new Rect(0, 0, width, height));

            var sourceWidth = Math.Max(1, source.Width);
            var sourceHeight = Math.Max(1, source.Height);
            var scale = Math.Min(width / sourceWidth, height / sourceHeight);
            var targetWidth = sourceWidth * scale;
            var targetHeight = sourceHeight * scale;
            var x = (width - targetWidth) / 2;
            var y = (height - targetHeight) / 2;
            context.DrawImage(source, new Rect(x, y, targetWidth, targetHeight));
        }

        var bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
        bitmap.Render(visual);
        bitmap.Freeze();
        return bitmap;
    }
}
