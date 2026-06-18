using StroyMaterials.App.Models;

namespace StroyMaterials.App;

internal sealed class OrderListForm : Form
{
    private readonly UserSession _session;
    private readonly DataGridView _grid = new();
    private readonly Label _countLabel = new();

    public OrderListForm(UserSession session)
    {
        _session = session;
        Theme.ApplyForm(this, "ООО \"СтройМатериалы\" - заказы");
        Size = new Size(1120, 650);
        Controls.Add(CreateLayout());
        ConfigureGrid();
        LoadOrders();
    }

    private Control CreateLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 3,
            ColumnCount = 1
        };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

        var header = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            BackColor = Theme.SecondaryBackground,
            Padding = new Padding(12, 8, 12, 8),
            ColumnCount = 5
        };
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));

        header.Controls.Add(new Label
        {
            Text = "Заказы",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = Theme.Bold(18F)
        }, 0, 0);

        var addButton = Theme.AccentButton("Добавить заказ");
        addButton.Visible = _session.CanEditOrders;
        addButton.Click += (_, _) => AddOrder();

        var editButton = Theme.AccentButton("Редактировать");
        editButton.Visible = _session.CanEditOrders;
        editButton.Click += (_, _) => EditSelectedOrder();

        var deleteButton = Theme.AccentButton("Удалить заказ");
        deleteButton.Visible = _session.CanEditOrders;
        deleteButton.Click += (_, _) => DeleteSelectedOrder();

        var closeButton = new Button
        {
            Text = "Назад",
            Dock = DockStyle.Fill,
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.White,
            Font = Theme.Bold()
        };
        closeButton.Click += (_, _) => Close();

        header.Controls.Add(addButton, 1, 0);
        header.Controls.Add(editButton, 2, 0);
        header.Controls.Add(deleteButton, 3, 0);
        header.Controls.Add(closeButton, 4, 0);

        root.Controls.Add(header, 0, 0);
        root.Controls.Add(_grid, 0, 1);
        _countLabel.Dock = DockStyle.Fill;
        _countLabel.Padding = new Padding(12, 0, 0, 0);
        root.Controls.Add(_countLabel, 0, 2);
        return root;
    }

    private void ConfigureGrid()
    {
        Theme.StyleGrid(_grid);
        _grid.Dock = DockStyle.Fill;
        _grid.CellDoubleClick += (_, _) =>
        {
            if (_session.CanEditOrders)
            {
                EditSelectedOrder();
            }
        };

        _grid.Columns.Add("Id", "Номер");
        _grid.Columns.Add("Items", "Артикулы");
        _grid.Columns.Add("Status", "Статус");
        _grid.Columns.Add("Pickup", "Пункт выдачи");
        _grid.Columns.Add("OrderDate", "Дата заказа");
        _grid.Columns.Add("DeliveryDate", "Дата выдачи");
        _grid.Columns.Add("Customer", "Клиент");
        _grid.Columns.Add("ReceiveCode", "Код");

        _grid.Columns["Id"]!.FillWeight = 45;
        _grid.Columns["Items"]!.FillWeight = 150;
        _grid.Columns["Pickup"]!.FillWeight = 210;
        _grid.Columns["ReceiveCode"]!.FillWeight = 55;
    }

    private void LoadOrders()
    {
        var orders = Database.GetOrders();
        _grid.Rows.Clear();
        foreach (var order in orders)
        {
            var rowIndex = _grid.Rows.Add(
                order.Id,
                order.ItemsText,
                order.StatusName,
                order.PickupPointAddress,
                order.OrderDate.ToString("dd.MM.yyyy"),
                order.DeliveryDate.ToString("dd.MM.yyyy"),
                order.CustomerName,
                order.ReceiveCode);
            _grid.Rows[rowIndex].Tag = order;
        }

        _countLabel.Text = $"Показано заказов: {orders.Count}";
    }

    private OrderRecord? SelectedOrder => _grid.CurrentRow?.Tag as OrderRecord;

    private void AddOrder()
    {
        using var form = new OrderEditForm(null);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            LoadOrders();
        }
    }

    private void EditSelectedOrder()
    {
        if (SelectedOrder is not { } order)
        {
            MessageBox.Show("Выберите заказ для редактирования.", "Редактирование заказа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var form = new OrderEditForm(order.Id);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            LoadOrders();
        }
    }

    private void DeleteSelectedOrder()
    {
        if (SelectedOrder is not { } order)
        {
            MessageBox.Show("Выберите заказ для удаления.", "Удаление заказа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var answer = MessageBox.Show($"Удалить заказ №{order.Id}?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (answer != DialogResult.Yes)
        {
            return;
        }

        Database.DeleteOrder(order.Id);
        LoadOrders();
    }
}
