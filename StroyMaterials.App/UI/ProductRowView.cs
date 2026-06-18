using System.Windows.Media;
using StroyMaterials.App.Models;

namespace StroyMaterials.App.UI;

internal sealed class ProductRowView
{
    public ProductRowView(ProductRecord product)
    {
        Product = product;
        Image = ImageTools.LoadProductImage(product.ImagePath, 220);
    }

    public ProductRecord Product { get; }

    public ImageSource Image { get; }

    public string Article => Product.Article;

    public string Name => Product.Name;

    public string CategoryName => Product.CategoryName;

    public string Description => Product.Description;

    public string ManufacturerName => Product.ManufacturerName;

    public string SupplierName => Product.SupplierName;

    public string UnitName => Product.UnitName;

    public int StockQuantity => Product.StockQuantity;

    public int Discount => Product.Discount;

    public string PriceText => Product.Price.ToString("0.00");

    public string FinalPriceText => HasDiscount ? Product.FinalPrice.ToString("0.00") : "";

    public string DiscountText => $"{Product.Discount}%";

    public bool HasDiscount => Product.Discount > 0;

    public Brush RowBackground => Product.StockQuantity == 0
        ? Theme.OutOfStockBackgroundBrush
        : Product.Discount > 12
            ? Theme.DiscountBackgroundBrush
            : Theme.MainBackgroundBrush;
}
