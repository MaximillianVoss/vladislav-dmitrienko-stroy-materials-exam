using System.Windows.Media;

namespace StroyMaterials.App;

internal static class Theme
{
    public const string MainBackground = "#FFFFFF";
    public const string SecondaryBackground = "#DAA520";
    public const string Accent = "#B8860B";
    public const string DiscountBackground = "#F4A460";
    public const string OutOfStockBackground = "#D9EFFB";

    public static readonly Brush MainBackgroundBrush = FrozenBrush(MainBackground);
    public static readonly Brush SecondaryBackgroundBrush = FrozenBrush(SecondaryBackground);
    public static readonly Brush AccentBrush = FrozenBrush(Accent);
    public static readonly Brush DiscountBackgroundBrush = FrozenBrush(DiscountBackground);
    public static readonly Brush OutOfStockBackgroundBrush = FrozenBrush(OutOfStockBackground);

    private static Brush FrozenBrush(string color)
    {
        var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
        brush.Freeze();
        return brush;
    }
}
