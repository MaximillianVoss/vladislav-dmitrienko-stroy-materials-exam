using StroyMaterials.App.Models;

namespace StroyMaterials.App;

internal sealed class OrderEditForm : Form
{
    private readonly bool _isNew;
    private readonly OrderRecord _order;

    private readonly TextBox _itemsTextBox = new();
    private readonly ComboBox _statusComboBox = new();
    private readonly ComboBox _pickupComboBox = new();
    private readonly DateTimePicker _orderDatePicker = new();
    private readonly DateTimePicker _deliveryDatePicker = new();
    private readonly ComboBox _customerComboBox = new();
    private readonly NumericUpDown _receiveCodeBox = new();

    public OrderEditForm(int? orderId)
    {
        _isNew = orderId is null;
        _order = orderId is null ? new OrderRecord() : Database.GetOrder(orderId.Value) ?? new OrderRecord();

        Theme.ApplyForm(this, _isNew ? "Добавление заказа" : $"Редактирование заказа №{_order.Id}");
        Size = new Size(780, 520);
        MinimumSize = new Size(720, 500);

        Controls.Add(CreateLayout());
        LoadLookups();
        FillForm();
    }

    private Control CreateLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(18),
            RowCount = 2,
            ColumnCount = 1
        };
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));

        var fields = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 7
        };
        fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190));
        fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        AddField(fields, 0, "Артикулы и количество", _itemsTextBox, 82);
        AddField(fields, 1, "Статус заказа", _statusComboBox);
        AddField(fields, 2, "Адрес пункта выдачи", _pickupComboBox);
        AddField(fields, 3, "Дата заказа", _orderDatePicker);
        AddField(fields, 4, "Дата выдачи", _deliveryDatePicker);
        AddField(fields, 5, "Клиент", _customerComboBox);
        AddField(fields, 6, "Код получения", _receiveCodeBox);

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft
        };
        var saveButton = Theme.AccentButton("Сохранить");
        saveButton.Width = 130;
        saveButton.Click += (_, _) => Save();
        var cancelButton = new Button
        {
            Text = "Отмена",
            Width = 110,
            Height = 34,
            FlatStyle = FlatStyle.Flat
        };
        cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
        buttons.Controls.Add(saveButton);
        buttons.Controls.Add(cancelButton);

        root.Controls.Add(fields, 0, 0);
        root.Controls.Add(buttons, 0, 1);
        return root;
    }

    private static void AddField(TableLayoutPanel panel, int row, string labelText, Control control, int height = 46)
    {
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
        panel.Controls.Add(new Label
        {
            Text = labelText,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = Theme.Bold()
        }, 0, row);

        control.Dock = DockStyle.Fill;
        if (control is ComboBox comboBox)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
        }
        if (control is TextBox textBox)
        {
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
        }

        panel.Controls.Add(control, 1, row);
    }

    private void LoadLookups()
    {
        BindLookup(_statusComboBox, "order_statuses");
        BindLookup(_pickupComboBox, "pickup_points");

        _customerComboBox.DataSource = Database.GetCustomers();
        _customerComboBox.DropDownStyle = ComboBoxStyle.DropDown;
    }

    private static void BindLookup(ComboBox comboBox, string tableName)
    {
        comboBox.DataSource = Database.GetLookupItems(tableName);
        comboBox.DisplayMember = nameof(LookupItem.Name);
        comboBox.ValueMember = nameof(LookupItem.Id);
        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    }

    private void FillForm()
    {
        _itemsTextBox.Text = _order.ItemsText;
        _orderDatePicker.Format = DateTimePickerFormat.Short;
        _deliveryDatePicker.Format = DateTimePickerFormat.Short;
        _orderDatePicker.Value = _order.OrderDate;
        _deliveryDatePicker.Value = _order.DeliveryDate;
        _receiveCodeBox.Minimum = 100;
        _receiveCodeBox.Maximum = 999999;
        _receiveCodeBox.Value = _order.ReceiveCode > 0 ? _order.ReceiveCode : 100;
        _customerComboBox.Text = _order.CustomerName;

        SelectLookup(_statusComboBox, _order.StatusId);
        SelectLookup(_pickupComboBox, _order.PickupPointId);
        if (_pickupComboBox.SelectedItem is null && _pickupComboBox.Items.Count > 0)
        {
            _pickupComboBox.SelectedIndex = 0;
        }
        if (_statusComboBox.SelectedItem is null && _statusComboBox.Items.Count > 0)
        {
            _statusComboBox.SelectedIndex = 0;
        }
    }

    private static void SelectLookup(ComboBox comboBox, int id)
    {
        foreach (var item in comboBox.Items.OfType<LookupItem>())
        {
            if (item.Id == id)
            {
                comboBox.SelectedItem = item;
                return;
            }
        }
    }

    private void Save()
    {
        if (!TryParseItems(_itemsTextBox.Text, out var items, out var error))
        {
            MessageBox.Show(error, "Ошибка заказа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(_customerComboBox.Text))
        {
            MessageBox.Show("Укажите клиента.", "Ошибка заказа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_deliveryDatePicker.Value.Date < _orderDatePicker.Value.Date)
        {
            MessageBox.Show("Дата выдачи не может быть раньше даты заказа.", "Ошибка заказа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _order.ItemsText = _itemsTextBox.Text.Trim();
        _order.StatusId = SelectedLookupId(_statusComboBox);
        _order.PickupPointId = SelectedLookupId(_pickupComboBox);
        _order.OrderDate = _orderDatePicker.Value.Date;
        _order.DeliveryDate = _deliveryDatePicker.Value.Date;
        _order.CustomerName = _customerComboBox.Text.Trim();
        _order.ReceiveCode = (int)_receiveCodeBox.Value;

        Database.SaveOrder(_order, items, _isNew);
        DialogResult = DialogResult.OK;
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

    private static int SelectedLookupId(ComboBox comboBox)
    {
        return comboBox.SelectedItem is LookupItem item ? item.Id : 0;
    }
}
