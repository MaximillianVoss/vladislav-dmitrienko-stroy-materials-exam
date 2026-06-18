using System.Windows;
using System.Windows.Input;
using StroyMaterials.App.Models;
using StroyMaterials.App.UI;

namespace StroyMaterials.App.Views;

public partial class MainWindow : Window
{
    private readonly UserSession _session;
    private bool _isInitialized;

    public MainWindow(UserSession session)
    {
        _session = session;
        InitializeComponent();

        LogoImage.Source = ImageTools.LoadLogo(80);
        UserTextBlock.Text = _session.FullName;

        FiltersPanel.Visibility = _session.CanFilterProducts ? Visibility.Visible : Visibility.Collapsed;
        AddProductButton.Visibility = _session.CanEditProducts ? Visibility.Visible : Visibility.Collapsed;
        EditProductButton.Visibility = _session.CanEditProducts ? Visibility.Visible : Visibility.Collapsed;
        DeleteProductButton.Visibility = _session.CanEditProducts ? Visibility.Visible : Visibility.Collapsed;
        OrdersButton.Visibility = _session.CanViewOrders ? Visibility.Visible : Visibility.Collapsed;

        LoadFilters();
        _isInitialized = true;
        LoadProducts();
    }

    private void LoadFilters()
    {
        if (!_session.CanFilterProducts)
        {
            return;
        }

        var manufacturers = new List<LookupItem> { new() { Id = 0, Name = "Все производители" } };
        manufacturers.AddRange(Database.GetLookupItems("manufacturers"));
        ManufacturerComboBox.ItemsSource = manufacturers;
        ManufacturerComboBox.SelectedIndex = 0;

        SortComboBox.ItemsSource = new List<SortOption>
        {
            new("none", "Без сортировки"),
            new("stock_asc", "Остаток по возрастанию"),
            new("stock_desc", "Остаток по убыванию"),
            new("price_asc", "Цена по возрастанию"),
            new("price_desc", "Цена по убыванию"),
            new("discount_asc", "Скидка по возрастанию"),
            new("discount_desc", "Скидка по убыванию")
        };
        SortComboBox.SelectedIndex = 0;
    }

    private void LoadProducts()
    {
        var manufacturer = ManufacturerComboBox.SelectedItem as LookupItem;
        var sort = SortComboBox.SelectedItem as SortOption;
        var products = Database
            .GetProducts(
                _session.CanFilterProducts ? SearchTextBox.Text : "",
                _session.CanFilterProducts && manufacturer?.Id > 0 ? manufacturer.Id : null,
                _session.CanFilterProducts ? sort?.Key ?? "none" : "none")
            .Select(product => new ProductRowView(product))
            .ToList();

        ProductsGrid.ItemsSource = products;
        CountTextBlock.Text = $"Показано товаров: {products.Count}";
    }

    private ProductRecord? SelectedProduct => (ProductsGrid.SelectedItem as ProductRowView)?.Product;

    private void FilterControl_Changed(object sender, EventArgs e)
    {
        if (_isInitialized)
        {
            LoadProducts();
        }
    }

    private void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new ProductEditWindow(null) { Owner = this };
        if (window.ShowDialog() == true)
        {
            LoadProducts();
        }
    }

    private void EditProductButton_Click(object sender, RoutedEventArgs e)
    {
        EditSelectedProduct();
    }

    private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedProduct is not { } product)
        {
            MessageBox.Show(this, "Выберите товар для удаления.", "Удаление товара", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        if (Database.ProductIsUsedInOrders(product.Article))
        {
            MessageBox.Show(this, "Товар присутствует в заказе, поэтому удалить его нельзя.", "Удаление запрещено", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var answer = MessageBox.Show(this, $"Удалить товар \"{product.Name}\"?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (answer != MessageBoxResult.Yes)
        {
            return;
        }

        Database.DeleteProduct(product.Article);
        ImageTools.DeleteProductImageIfCustom(product.ImagePath);
        LoadProducts();
    }

    private void OrdersButton_Click(object sender, RoutedEventArgs e)
    {
        new OrderListWindow(_session) { Owner = this }.ShowDialog();
    }

    private void ProductsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (_session.CanEditProducts)
        {
            EditSelectedProduct();
        }
    }

    private void EditSelectedProduct()
    {
        if (SelectedProduct is not { } product)
        {
            MessageBox.Show(this, "Выберите товар для редактирования.", "Редактирование товара", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var window = new ProductEditWindow(product.Article) { Owner = this };
        if (window.ShowDialog() == true)
        {
            LoadProducts();
        }
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        new LoginWindow().Show();
        Close();
    }
}
