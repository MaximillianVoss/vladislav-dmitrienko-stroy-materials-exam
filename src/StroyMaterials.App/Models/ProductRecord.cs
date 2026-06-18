namespace StroyMaterials.App.Models;

public sealed class ProductRecord
{
    public string Article { get; set; } = "";

    public string Name { get; set; } = "";

    public int UnitId { get; set; }

    public string UnitName { get; set; } = "";

    public decimal Price { get; set; }

    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = "";

    public int ManufacturerId { get; set; }

    public string ManufacturerName { get; set; } = "";

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = "";

    public int Discount { get; set; }

    public int StockQuantity { get; set; }

    public string Description { get; set; } = "";

    public string ImagePath { get; set; } = "picture.png";

    public decimal FinalPrice => Math.Round(Price * (100 - Discount) / 100, 2);
}
