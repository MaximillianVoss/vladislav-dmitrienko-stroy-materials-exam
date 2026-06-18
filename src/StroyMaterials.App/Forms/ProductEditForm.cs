using StroyMaterials.App.Models;

namespace StroyMaterials.App;

internal sealed class ProductEditForm : Form
{
    private readonly bool _isNew;
    private readonly string _article;
    private readonly ProductRecord? _originalProduct;

    private readonly TextBox _articleTextBox = new();
    private readonly TextBox _nameTextBox = new();
    private readonly ComboBox _categoryComboBox = new();
    private readonly TextBox _descriptionTextBox = new();
    private readonly ComboBox _manufacturerComboBox = new();
    private readonly ComboBox _supplierComboBox = new();
    private readonly NumericUpDown _priceBox = new();
    private readonly ComboBox _unitComboBox = new();
    private readonly NumericUpDown _stockBox = new();
    private readonly NumericUpDown _discountBox = new();
    private readonly PictureBox _imagePreview = new();

    private string? _selectedImageFile;
    private string _imagePath = "picture.png";

    public ProductEditForm(string? article)
    {
        _isNew = article is null;
        _article = article ?? Database.GetNextProductArticle();
        _originalProduct = article is null ? null : Database.GetProduct(article);
        _imagePath = _originalProduct?.ImagePath ?? "picture.png";

        Theme.ApplyForm(this, _isNew ? "Добавление товара" : "Редактирование товара");
        Size = new Size(820, 650);
        MinimumSize = new Size(760, 620);

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
            RowCount = 11
        };
        fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190));
        fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        AddField(fields, 0, "Артикул", _articleTextBox, !_isNew);
        AddField(fields, 1, "Наименование", _nameTextBox);
        AddField(fields, 2, "Категория", _categoryComboBox);
        AddField(fields, 3, "Описание", _descriptionTextBox);
        AddField(fields, 4, "Производитель", _manufacturerComboBox);
        AddField(fields, 5, "Поставщик", _supplierComboBox);
        AddField(fields, 6, "Цена", _priceBox);
        AddField(fields, 7, "Единица измерения", _unitComboBox);
        AddField(fields, 8, "Количество на складе", _stockBox);
        AddField(fields, 9, "Действующая скидка, %", _discountBox);
        AddImageField(fields, 10);

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

    private static void AddField(TableLayoutPanel panel, int row, string labelText, Control control, bool visible = true)
    {
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, row == 3 ? 96 : 42));

        var label = new Label
        {
            Text = labelText,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = Theme.Bold(),
            Visible = visible
        };

        control.Dock = DockStyle.Fill;
        control.Visible = visible;
        if (control is ComboBox comboBox)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        if (control is TextBox textBox && row == 3)
        {
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
        }

        panel.Controls.Add(label, 0, row);
        panel.Controls.Add(control, 1, row);
    }

    private void AddImageField(TableLayoutPanel panel, int row)
    {
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 112));
        panel.Controls.Add(new Label
        {
            Text = "Фото товара",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = Theme.Bold()
        }, 0, row);

        var imagePanel = new FlowLayoutPanel { Dock = DockStyle.Fill };
        _imagePreview.Size = new Size(132, 88);
        _imagePreview.SizeMode = PictureBoxSizeMode.Zoom;
        _imagePreview.BorderStyle = BorderStyle.FixedSingle;

        var chooseButton = new Button
        {
            Text = "Выбрать фото",
            Width = 136,
            Height = 34,
            FlatStyle = FlatStyle.Flat,
            BackColor = Theme.SecondaryBackground,
            Font = Theme.Bold()
        };
        chooseButton.Click += (_, _) => ChooseImage();
        imagePanel.Controls.Add(_imagePreview);
        imagePanel.Controls.Add(chooseButton);
        panel.Controls.Add(imagePanel, 1, row);
    }

    private void LoadLookups()
    {
        BindLookup(_categoryComboBox, "categories");
        BindLookup(_manufacturerComboBox, "manufacturers");
        BindLookup(_supplierComboBox, "suppliers");
        BindLookup(_unitComboBox, "units");
    }

    private static void BindLookup(ComboBox comboBox, string tableName)
    {
        comboBox.DataSource = Database.GetLookupItems(tableName);
        comboBox.DisplayMember = nameof(LookupItem.Name);
        comboBox.ValueMember = nameof(LookupItem.Id);
    }

    private void FillForm()
    {
        _articleTextBox.Text = _article;
        _articleTextBox.ReadOnly = true;

        _priceBox.DecimalPlaces = 2;
        _priceBox.Maximum = 10_000_000;
        _priceBox.Minimum = 0;
        _priceBox.ThousandsSeparator = true;

        _stockBox.Maximum = 1_000_000;
        _stockBox.Minimum = 0;

        _discountBox.Maximum = 100;
        _discountBox.Minimum = 0;

        if (_originalProduct is not null)
        {
            _nameTextBox.Text = _originalProduct.Name;
            _descriptionTextBox.Text = _originalProduct.Description;
            _priceBox.Value = _originalProduct.Price;
            _stockBox.Value = _originalProduct.StockQuantity;
            _discountBox.Value = _originalProduct.Discount;
            SelectLookup(_categoryComboBox, _originalProduct.CategoryId);
            SelectLookup(_manufacturerComboBox, _originalProduct.ManufacturerId);
            SelectLookup(_supplierComboBox, _originalProduct.SupplierId);
            SelectLookup(_unitComboBox, _originalProduct.UnitId);
        }

        LoadPreviewFromRelativePath(_imagePath);
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

    private void LoadPreviewFromRelativePath(string relativePath)
    {
        _imagePreview.Image?.Dispose();
        _imagePreview.Image = ImageTools.LoadProductImage(relativePath, 132, 88);
    }

    private void ChooseImage()
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp",
            Title = "Выберите изображение товара"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _selectedImageFile = dialog.FileName;
        _imagePreview.Image?.Dispose();
        using var source = Image.FromFile(dialog.FileName);
        _imagePreview.Image = Theme.ResizeImage(source, 132, 88);
    }

    private void Save()
    {
        if (!ValidateForm(out var message))
        {
            MessageBox.Show(message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var oldImagePath = _imagePath;
        if (_selectedImageFile is not null)
        {
            _imagePath = ImageTools.SaveProductImage(_selectedImageFile, _article);
        }

        var product = new ProductRecord
        {
            Article = _article,
            Name = _nameTextBox.Text.Trim(),
            CategoryId = SelectedLookupId(_categoryComboBox),
            Description = _descriptionTextBox.Text.Trim(),
            ManufacturerId = SelectedLookupId(_manufacturerComboBox),
            SupplierId = SelectedLookupId(_supplierComboBox),
            Price = _priceBox.Value,
            UnitId = SelectedLookupId(_unitComboBox),
            StockQuantity = (int)_stockBox.Value,
            Discount = (int)_discountBox.Value,
            ImagePath = _imagePath
        };

        Database.SaveProduct(product, _isNew);
        if (_selectedImageFile is not null && oldImagePath != _imagePath)
        {
            ImageTools.DeleteProductImageIfCustom(oldImagePath);
        }

        DialogResult = DialogResult.OK;
    }

    private bool ValidateForm(out string message)
    {
        if (string.IsNullOrWhiteSpace(_nameTextBox.Text))
        {
            message = "Введите наименование товара.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(_descriptionTextBox.Text))
        {
            message = "Введите описание товара.";
            return false;
        }

        if (SelectedLookupId(_categoryComboBox) == 0 || SelectedLookupId(_manufacturerComboBox) == 0 ||
            SelectedLookupId(_supplierComboBox) == 0 || SelectedLookupId(_unitComboBox) == 0)
        {
            message = "Заполните все справочники товара.";
            return false;
        }

        message = "";
        return true;
    }

    private static int SelectedLookupId(ComboBox comboBox)
    {
        return comboBox.SelectedItem is LookupItem item ? item.Id : 0;
    }
}
