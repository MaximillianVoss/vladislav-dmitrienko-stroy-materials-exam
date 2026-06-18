namespace StroyMaterials.App;

internal static class AppPaths
{
    public static string DataDirectory => Path.Combine(AppContext.BaseDirectory, "Data");

    public static string DatabasePath => Path.Combine(DataDirectory, "stroymaterials.db");

    public static string DatabaseDirectory => Path.Combine(AppContext.BaseDirectory, "Database");

    public static string AppAssetsDirectory => Path.Combine(AppContext.BaseDirectory, "Assets", "App");

    public static string ProductImagesDirectory => Path.Combine(AppContext.BaseDirectory, "Assets", "Products");

    public static string PlaceholderImage => Path.Combine(ProductImagesDirectory, "picture.png");

    public static string AppIcon => Path.Combine(AppAssetsDirectory, "icon.ico");

    public static string Logo => Path.Combine(AppAssetsDirectory, "icon.png");
}
