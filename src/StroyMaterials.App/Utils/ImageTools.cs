namespace StroyMaterials.App;

internal static class ImageTools
{
    public static Image LoadProductImage(string relativePath, int width = 90, int height = 64)
    {
        var path = Path.Combine(AppPaths.ProductImagesDirectory, relativePath);
        if (!File.Exists(path))
        {
            path = AppPaths.PlaceholderImage;
        }

        using var stream = File.OpenRead(path);
        using var source = Image.FromStream(stream);
        return Theme.ResizeImage(source, width, height);
    }

    public static string SaveProductImage(string sourcePath, string article)
    {
        Directory.CreateDirectory(AppPaths.ProductImagesDirectory);
        var fileName = $"{article}_{DateTime.Now:yyyyMMddHHmmss}.png";
        var targetPath = Path.Combine(AppPaths.ProductImagesDirectory, fileName);

        using var source = Image.FromFile(sourcePath);
        using var resized = Theme.ResizeImage(source, 300, 200);
        resized.Save(targetPath, System.Drawing.Imaging.ImageFormat.Png);
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
}
