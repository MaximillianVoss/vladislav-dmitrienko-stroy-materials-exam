using System.Globalization;
using System.Windows;
using Microsoft.Win32;
using StroyMaterials.App.Models;

namespace StroyMaterials.App.Views;

public partial class ProductEditWindow : Window
{
    private readonly bool _isNew;
    private readonly string _article;
    private readonly ProductRecord? _originalProduct;
    private string? _selectedImageFile;
    private string _imagePath = "picture.png";

    public ProductEditWindow(string? article)
    {
        _isNew = article is null;
        _article = article ?? Database.GetNextProductArticle();
        _originalProduct = article is null ? null : Database.GetProduct(article);
        _imagePath = _originalProduct?.ImagePath ?? "picture.png";

        InitializeComponent();
        Title = _isNew ? "Добавление товара" : "Редактирование товара";
        TitleTextBlock.Text = Title;
        LoadLookups();
        FillForm();
    }

    private void LoadLookups()
    {
        CategoryComboBox.ItemsSource = Database.GetLookupItems("categories");
        ManufacturerComboBox.ItemsSource = Database.GetLookupItems("manufacturers");
        SupplierComboBox.ItemsSource = Database.GetLookupItems("suppliers");
        UnitComboBox.ItemsSource = Database.GetLookupItems("units");
    }

    private void FillForm()
    {
        ArticleTextBox.Text = _article;
        ArticleLabel.Visibility = _isNew ? Visibility.Collapsed : Visibility.Visible;
        ArticleTextBox.Visibility = _isNew ? Visibility.Collapsed : Visibility.Visible;
        ArticleRow.Height = _isNew ? new GridLength(0) : GridLength.Auto;

        PriceTextBox.Text = "0,00";
        StockTextBox.Text = "0";
        DiscountTextBox.Text = "0";

        if (_originalProduct is not null)
        {
            NameTextBox.Text = _originalProduct.Name;
            DescriptionTextBox.Text = _originalProduct.Description;
            PriceTextBox.Text = _originalProduct.Price.ToString("0.00");
            StockTextBox.Text = _originalProduct.StockQuantity.ToString(CultureInfo.InvariantCulture);
            DiscountTextBox.Text = _originalProduct.Discount.ToString(CultureInfo.InvariantCulture);
            SelectLookup(CategoryComboBox, _originalProduct.CategoryId);
            SelectLookup(ManufacturerComboBox, _originalProduct.ManufacturerId);
            SelectLookup(SupplierComboBox, _originalProduct.SupplierId);
            SelectLookup(UnitComboBox, _originalProduct.UnitId);
        }
        else
        {
            SelectFirst(CategoryComboBox);
            SelectFirst(ManufacturerComboBox);
            SelectFirst(SupplierComboBox);
            SelectFirst(UnitComboBox);
        }

        ImagePreview.Source = ImageTools.LoadProductImage(_imagePath, 320);
    }

    private static void SelectLookup(System.Windows.Controls.ComboBox comboBox, int id)
    {
        foreach (var item in comboBox.Items.OfType<LookupItem>())
        {
            if (item.Id == id)
            {
                comboBox.SelectedItem = item;
                return;
            }
        }

        SelectFirst(comboBox);
    }

    private static void SelectFirst(System.Windows.Controls.ComboBox comboBox)
    {
        if (comboBox.Items.Count > 0)
        {
            comboBox.SelectedIndex = 0;
        }
    }

    private void ChooseImageButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp",
            Title = "Выберите изображение товара"
        };

        if (dialog.ShowDialog(this) != true)
        {
            return;
        }

        _selectedImageFile = dialog.FileName;
        ImagePreview.Source = ImageTools.LoadBitmap(dialog.FileName, 320);
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateForm(out var product, out var message))
        {
            MessageBox.Show(this, message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var oldImagePath = _imagePath;
        if (_selectedImageFile is not null)
        {
            _imagePath = ImageTools.SaveProductImage(_selectedImageFile, _article);
            product.ImagePath = _imagePath;
        }

        Database.SaveProduct(product, _isNew);
        if (_selectedImageFile is not null && oldImagePath != _imagePath)
        {
            ImageTools.DeleteProductImageIfCustom(oldImagePath);
        }

        DialogResult = true;
    }

    private bool ValidateForm(out ProductRecord product, out string message)
    {
        product = new ProductRecord();
        message = "";

        if (string.IsNullOrWhiteSpace(NameTextBox.Text))
        {
            message = "Введите наименование товара.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
        {
            message = "Введите описание товара.";
            return false;
        }

        if (!TryParsePrice(PriceTextBox.Text, out var price))
        {
            message = "Цена должна быть неотрицательным числом.";
            return false;
        }

        if (!int.TryParse(StockTextBox.Text.Trim(), out var stock) || stock < 0)
        {
            message = "Количество на складе должно быть неотрицательным целым числом.";
            return false;
        }

        if (!int.TryParse(DiscountTextBox.Text.Trim(), out var discount) || discount < 0 || discount > 100)
        {
            message = "Скидка должна быть целым числом от 0 до 100.";
            return false;
        }

        var categoryId = SelectedLookupId(CategoryComboBox);
        var manufacturerId = SelectedLookupId(ManufacturerComboBox);
        var supplierId = SelectedLookupId(SupplierComboBox);
        var unitId = SelectedLookupId(UnitComboBox);
        if (categoryId == 0 || manufacturerId == 0 || supplierId == 0 || unitId == 0)
        {
            message = "Заполните все справочники товара.";
            return false;
        }

        product = new ProductRecord
        {
            Article = _article,
            Name = NameTextBox.Text.Trim(),
            CategoryId = categoryId,
            Description = DescriptionTextBox.Text.Trim(),
            ManufacturerId = manufacturerId,
            SupplierId = supplierId,
            Price = price,
            UnitId = unitId,
            StockQuantity = stock,
            Discount = discount,
            ImagePath = _imagePath
        };
        return true;
    }

    private static bool TryParsePrice(string value, out decimal price)
    {
        var normalized = value.Trim().Replace(',', '.');
        return decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out price) && price >= 0;
    }

    private static int SelectedLookupId(System.Windows.Controls.ComboBox comboBox)
    {
        return comboBox.SelectedItem is LookupItem item ? item.Id : 0;
    }
}
