using StroyMaterials.App.Models;

namespace StroyMaterials.App;

internal sealed class MainForm : Form
{
    private readonly UserSession _session;
    private readonly DataGridView _grid = new();
    private readonly TextBox _searchTextBox = new();
    private readonly ComboBox _manufacturerComboBox = new();
    private readonly ComboBox _sortComboBox = new();
    private readonly Label _countLabel = new();

    public MainForm(UserSession session)
    {
        _session = session;
        Theme.ApplyForm(this, "ООО \"СтройМатериалы\" - товары");
        Size = new Size(1220, 740);

        Controls.Add(CreateRootLayout());
        ConfigureGrid();
        LoadFilters();
        LoadProducts();
    }

    private Control CreateRootLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 3,
            ColumnCount = 1
        };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 74));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, _session.CanFilterProducts ? 58 : 0));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        root.Controls.Add(CreateHeader(), 0, 0);
        root.Controls.Add(CreateFilterPanel(), 0, 1);
        root.Controls.Add(CreateGridPanel(), 0, 2);
        return root;
    }

    private Control CreateHeader()
    {
        var header = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            BackColor = Theme.SecondaryBackground,
            Padding = new Padding(12, 8, 12, 8),
            ColumnCount = 8
        };
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 58));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 156));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 156));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 156));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 98));

        var logo = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom };
        if (File.Exists(AppPaths.Logo))
        {
            using var image = Image.FromFile(AppPaths.Logo);
            logo.Image = new Bitmap(image);
        }

        var title = new Label
        {
            Text = "Список товаров",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = Theme.Bold(18F)
        };

        var addButton = Theme.AccentButton("Добавить товар");
        addButton.Visible = _session.CanEditProducts;
        addButton.Click += (_, _) => AddProduct();

        var editButton = Theme.AccentButton("Редактировать");
        editButton.Visible = _session.CanEditProducts;
        editButton.Click += (_, _) => EditSelectedProduct();

        var deleteButton = Theme.AccentButton("Удалить товар");
        deleteButton.Visible = _session.CanEditProducts;
        deleteButton.Click += (_, _) => DeleteSelectedProduct();

        var ordersButton = Theme.AccentButton("Заказы");
        ordersButton.Visible = _session.CanViewOrders;
        ordersButton.Click += (_, _) => ShowOrders();

        var userLabel = new Label
        {
            Text = _session.FullName,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleRight,
            Font = Theme.Bold(),
            AutoEllipsis = true
        };

        var logoutButton = new Button
        {
            Text = "Выход",
            Dock = DockStyle.Fill,
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.White,
            Font = Theme.Bold()
        };
        logoutButton.Click += (_, _) => Close();

        header.Controls.Add(logo, 0, 0);
        header.Controls.Add(title, 1, 0);
        header.Controls.Add(addButton, 2, 0);
        header.Controls.Add(editButton, 3, 0);
        header.Controls.Add(deleteButton, 4, 0);
        header.Controls.Add(ordersButton, 5, 0);
        header.Controls.Add(userLabel, 6, 0);
        header.Controls.Add(logoutButton, 7, 0);
        return header;
    }

    private Control CreateFilterPanel()
    {
        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12, 8, 12, 8),
            ColumnCount = 6,
            Visible = _session.CanFilterProducts
        };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 126));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28));

        _searchTextBox.Dock = DockStyle.Fill;
        _searchTextBox.PlaceholderText = "Поиск по артикулу, названию, описанию, категории, поставщику...";
        _searchTextBox.TextChanged += (_, _) => LoadProducts();

        _manufacturerComboBox.Dock = DockStyle.Fill;
        _manufacturerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _manufacturerComboBox.SelectedIndexChanged += (_, _) => LoadProducts();

        _sortComboBox.Dock = DockStyle.Fill;
        _sortComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _sortComboBox.SelectedIndexChanged += (_, _) => LoadProducts();

        panel.Controls.Add(CreateLabel("Поиск"), 0, 0);
        panel.Controls.Add(_searchTextBox, 1, 0);
        panel.Controls.Add(CreateLabel("Производитель"), 2, 0);
        panel.Controls.Add(_manufacturerComboBox, 3, 0);
        panel.Controls.Add(CreateLabel("Сортировка"), 4, 0);
        panel.Controls.Add(_sortComboBox, 5, 0);
        return panel;
    }

    private Control CreateGridPanel()
    {
        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            Padding = new Padding(12)
        };
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        panel.Controls.Add(_grid, 0, 0);
        _countLabel.Dock = DockStyle.Fill;
        _countLabel.TextAlign = ContentAlignment.MiddleLeft;
        panel.Controls.Add(_countLabel, 0, 1);
        return panel;
    }

    private static Label CreateLabel(string text) => new()
    {
        Text = text,
        Dock = DockStyle.Fill,
        TextAlign = ContentAlignment.MiddleLeft,
        Font = Theme.Bold()
    };

    private void ConfigureGrid()
    {
        Theme.StyleGrid(_grid);
        _grid.Dock = DockStyle.Fill;
        _grid.RowTemplate.Height = 72;
        _grid.CellDoubleClick += (_, _) =>
        {
            if (_session.CanEditProducts)
            {
                EditSelectedProduct();
            }
        };

        _grid.Columns.Add(new DataGridViewImageColumn
        {
            Name = "Image",
            HeaderText = "Фото",
            FillWeight = 55,
            ImageLayout = DataGridViewImageCellLayout.Zoom
        });
        _grid.Columns.Add("Article", "Артикул");
        _grid.Columns.Add("Name", "Наименование");
        _grid.Columns.Add("Category", "Категория");
        _grid.Columns.Add("Description", "Описание");
        _grid.Columns.Add("Manufacturer", "Производитель");
        _grid.Columns.Add("Supplier", "Поставщик");
        _grid.Columns.Add("Price", "Цена");
        _grid.Columns.Add("FinalPrice", "Итоговая цена");
        _grid.Columns.Add("Unit", "Ед.");
        _grid.Columns.Add("Stock", "Остаток");
        _grid.Columns.Add("Discount", "Скидка");

        _grid.Columns["Article"]!.FillWeight = 70;
        _grid.Columns["Name"]!.FillWeight = 120;
        _grid.Columns["Description"]!.FillWeight = 190;
        _grid.Columns["Unit"]!.FillWeight = 45;
        _grid.Columns["Stock"]!.FillWeight = 55;
        _grid.Columns["Discount"]!.FillWeight = 60;
    }

    private void LoadFilters()
    {
        if (!_session.CanFilterProducts)
        {
            return;
        }

        var manufacturers = new List<LookupItem> { new() { Id = 0, Name = "Все производители" } };
        manufacturers.AddRange(Database.GetLookupItems("manufacturers"));
        _manufacturerComboBox.DataSource = manufacturers;

        _sortComboBox.DataSource = new List<SortOption>
        {
            new("none", "Без сортировки"),
            new("stock_asc", "Остаток по возрастанию"),
            new("stock_desc", "Остаток по убыванию"),
            new("price_asc", "Цена по возрастанию"),
            new("price_desc", "Цена по убыванию"),
            new("discount_asc", "Скидка по возрастанию"),
            new("discount_desc", "Скидка по убыванию")
        };
    }

    private void LoadProducts()
    {
        var manufacturer = _manufacturerComboBox.SelectedItem as LookupItem;
        var sort = _sortComboBox.SelectedItem as SortOption;
        var products = Database.GetProducts(
            _session.CanFilterProducts ? _searchTextBox.Text : "",
            _session.CanFilterProducts && manufacturer?.Id > 0 ? manufacturer.Id : null,
            _session.CanFilterProducts ? sort?.Key ?? "none" : "none");

        _grid.Rows.Clear();
        foreach (var product in products)
        {
            var rowIndex = _grid.Rows.Add(
                ImageTools.LoadProductImage(product.ImagePath),
                product.Article,
                product.Name,
                product.CategoryName,
                product.Description,
                product.ManufacturerName,
                product.SupplierName,
                product.Price.ToString("0.00"),
                product.Discount > 0 ? product.FinalPrice.ToString("0.00") : "",
                product.UnitName,
                product.StockQuantity,
                $"{product.Discount}%");

            var row = _grid.Rows[rowIndex];
            row.Tag = product;
            if (product.StockQuantity == 0)
            {
                row.DefaultCellStyle.BackColor = Theme.OutOfStockBackground;
            }
            else if (product.Discount > 12)
            {
                row.DefaultCellStyle.BackColor = Theme.DiscountBackground;
            }

            if (product.Discount > 0)
            {
                row.Cells["Price"].Style.Font = new Font(_grid.Font, FontStyle.Strikeout);
                row.Cells["Price"].Style.ForeColor = Color.Red;
                row.Cells["FinalPrice"].Style.ForeColor = Color.Black;
            }
        }

        _countLabel.Text = $"Показано товаров: {products.Count}";
    }

    private ProductRecord? SelectedProduct => _grid.CurrentRow?.Tag as ProductRecord;

    private void AddProduct()
    {
        using var form = new ProductEditForm(null);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            LoadProducts();
        }
    }

    private void EditSelectedProduct()
    {
        if (SelectedProduct is not { } product)
        {
            MessageBox.Show("Выберите товар для редактирования.", "Редактирование товара", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var form = new ProductEditForm(product.Article);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            LoadProducts();
        }
    }

    private void DeleteSelectedProduct()
    {
        if (SelectedProduct is not { } product)
        {
            MessageBox.Show("Выберите товар для удаления.", "Удаление товара", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (Database.ProductIsUsedInOrders(product.Article))
        {
            MessageBox.Show("Товар присутствует в заказе, поэтому удалить его нельзя.", "Удаление запрещено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var answer = MessageBox.Show($"Удалить товар \"{product.Name}\"?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (answer != DialogResult.Yes)
        {
            return;
        }

        Database.DeleteProduct(product.Article);
        ImageTools.DeleteProductImageIfCustom(product.ImagePath);
        LoadProducts();
    }

    private void ShowOrders()
    {
        using var form = new OrderListForm(_session);
        form.ShowDialog(this);
    }

    private sealed record SortOption(string Key, string Text)
    {
        public override string ToString() => Text;
    }
}
