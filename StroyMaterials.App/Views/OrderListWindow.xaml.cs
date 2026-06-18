using System.Windows;
using System.Windows.Input;
using StroyMaterials.App.Models;

namespace StroyMaterials.App.Views;

public partial class OrderListWindow : Window
{
    private readonly UserSession _session;

    public OrderListWindow(UserSession session)
    {
        _session = session;
        InitializeComponent();

        AddOrderButton.Visibility = _session.CanEditOrders ? Visibility.Visible : Visibility.Collapsed;
        EditOrderButton.Visibility = _session.CanEditOrders ? Visibility.Visible : Visibility.Collapsed;
        DeleteOrderButton.Visibility = _session.CanEditOrders ? Visibility.Visible : Visibility.Collapsed;
        LoadOrders();
    }

    private OrderRecord? SelectedOrder => OrdersGrid.SelectedItem as OrderRecord;

    private void LoadOrders()
    {
        var orders = Database.GetOrders();
        OrdersGrid.ItemsSource = orders;
        CountTextBlock.Text = $"Показано заказов: {orders.Count}";
    }

    private void AddOrderButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new OrderEditWindow(null) { Owner = this };
        if (window.ShowDialog() == true)
        {
            LoadOrders();
        }
    }

    private void EditOrderButton_Click(object sender, RoutedEventArgs e)
    {
        EditSelectedOrder();
    }

    private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedOrder is not { } order)
        {
            MessageBox.Show(this, "Выберите заказ для удаления.", "Удаление заказа", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var answer = MessageBox.Show(this, $"Удалить заказ №{order.Id}?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (answer != MessageBoxResult.Yes)
        {
            return;
        }

        Database.DeleteOrder(order.Id);
        LoadOrders();
    }

    private void OrdersGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (_session.CanEditOrders)
        {
            EditSelectedOrder();
        }
    }

    private void EditSelectedOrder()
    {
        if (SelectedOrder is not { } order)
        {
            MessageBox.Show(this, "Выберите заказ для редактирования.", "Редактирование заказа", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var window = new OrderEditWindow(order.Id) { Owner = this };
        if (window.ShowDialog() == true)
        {
            LoadOrders();
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
