using System.Windows;
using StroyMaterials.App.Models;

namespace StroyMaterials.App.Views;

public partial class OrderEditWindow : Window
{
    private readonly bool _isNew;
    private readonly OrderRecord _order;

    public OrderEditWindow(int? orderId)
    {
        _isNew = orderId is null;
        _order = orderId is null ? new OrderRecord() : Database.GetOrder(orderId.Value) ?? new OrderRecord();

        InitializeComponent();
        Title = _isNew ? "Добавление заказа" : $"Редактирование заказа №{_order.Id}";
        TitleTextBlock.Text = Title;
        LoadLookups();
        FillForm();
    }

    private void LoadLookups()
    {
        StatusComboBox.ItemsSource = Database.GetLookupItems("order_statuses");
        PickupComboBox.ItemsSource = Database.GetLookupItems("pickup_points");
        CustomerComboBox.ItemsSource = Database.GetCustomers();
    }

    private void FillForm()
    {
        ItemsTextBox.Text = _order.ItemsText;
        OrderDatePicker.SelectedDate = _order.OrderDate == default ? DateTime.Today : _order.OrderDate;
        DeliveryDatePicker.SelectedDate = _order.DeliveryDate == default ? DateTime.Today : _order.DeliveryDate;
        ReceiveCodeTextBox.Text = (_order.ReceiveCode > 0 ? _order.ReceiveCode : 100).ToString();
        CustomerComboBox.Text = _order.CustomerName;

        SelectLookup(StatusComboBox, _order.StatusId);
        SelectLookup(PickupComboBox, _order.PickupPointId);
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

        if (comboBox.Items.Count > 0)
        {
            comboBox.SelectedIndex = 0;
        }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (!TryParseItems(ItemsTextBox.Text, out var items, out var error))
        {
            MessageBox.Show(this, error, "Ошибка заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(CustomerComboBox.Text))
        {
            MessageBox.Show(this, "Укажите клиента.", "Ошибка заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var orderDate = OrderDatePicker.SelectedDate?.Date ?? DateTime.Today;
        var deliveryDate = DeliveryDatePicker.SelectedDate?.Date ?? DateTime.Today;
        if (deliveryDate < orderDate)
        {
            MessageBox.Show(this, "Дата выдачи не может быть раньше даты заказа.", "Ошибка заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!int.TryParse(ReceiveCodeTextBox.Text.Trim(), out var receiveCode) || receiveCode <= 0)
        {
            MessageBox.Show(this, "Код получения должен быть положительным целым числом.", "Ошибка заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        _order.ItemsText = ItemsTextBox.Text.Trim();
        _order.StatusId = SelectedLookupId(StatusComboBox);
        _order.PickupPointId = SelectedLookupId(PickupComboBox);
        _order.OrderDate = orderDate;
        _order.DeliveryDate = deliveryDate;
        _order.CustomerName = CustomerComboBox.Text.Trim();
        _order.ReceiveCode = receiveCode;

        Database.SaveOrder(_order, items, _isNew);
        DialogResult = true;
    }

    private static bool TryParseItems(string text, out List<(string Article, int Quantity)> items, out string error)
    {
        items = new List<(string Article, int Quantity)>();
        error = "";

        var parts = text.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0 || parts.Length % 2 != 0)
        {
            error = "Введите артикулы в формате: PMEZMH, 2, BPV4MM, 1.";
            return false;
        }

        for (var i = 0; i < parts.Length; i += 2)
        {
            var article = parts[i];
            if (!int.TryParse(parts[i + 1], out var quantity) || quantity <= 0)
            {
                error = $"Количество для артикула {article} должно быть положительным целым числом.";
                return false;
            }

            if (!Database.ProductExists(article))
            {
                error = $"Артикул {article} отсутствует в базе товаров.";
                return false;
            }

            items.Add((article, quantity));
        }

        return true;
    }

    private static int SelectedLookupId(System.Windows.Controls.ComboBox comboBox)
    {
        return comboBox.SelectedItem is LookupItem item ? item.Id : 0;
    }
}
