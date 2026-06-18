using StroyMaterials.App.Models;

namespace StroyMaterials.App;

internal sealed class LoginForm : Form
{
    private readonly TextBox _loginTextBox = new();
    private readonly TextBox _passwordTextBox = new();

    public LoginForm()
    {
        Theme.ApplyForm(this, "ООО \"СтройМатериалы\" - вход");
        MinimumSize = new Size(520, 420);
        Size = new Size(520, 420);
        MaximizeBox = false;

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(28),
            RowCount = 6,
            ColumnCount = 1
        };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 96));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var logo = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.Zoom,
            Dock = DockStyle.Fill
        };
        if (File.Exists(AppPaths.Logo))
        {
            using var image = Image.FromFile(AppPaths.Logo);
            logo.Image = new Bitmap(image);
        }

        var title = new Label
        {
            Text = "ООО \"СтройМатериалы\"",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = Theme.Bold(16F)
        };

        _loginTextBox.PlaceholderText = "Логин";
        _loginTextBox.Dock = DockStyle.Bottom;
        _loginTextBox.Font = Theme.Regular(11F);

        _passwordTextBox.PlaceholderText = "Пароль";
        _passwordTextBox.UseSystemPasswordChar = true;
        _passwordTextBox.Dock = DockStyle.Bottom;
        _passwordTextBox.Font = Theme.Regular(11F);

        var loginButton = Theme.AccentButton("Войти");
        loginButton.Dock = DockStyle.Fill;
        loginButton.Click += (_, _) => Login();
        AcceptButton = loginButton;

        var guestButton = new Button
        {
            Text = "Продолжить как гость",
            Dock = DockStyle.Top,
            Height = 34,
            FlatStyle = FlatStyle.Flat,
            BackColor = Theme.SecondaryBackground,
            Font = Theme.Bold()
        };
        guestButton.Click += (_, _) => OpenMainForm(UserSession.Guest);

        root.Controls.Add(logo, 0, 0);
        root.Controls.Add(title, 0, 1);
        root.Controls.Add(WrapWithLabel("Логин", _loginTextBox), 0, 2);
        root.Controls.Add(WrapWithLabel("Пароль", _passwordTextBox), 0, 3);
        root.Controls.Add(loginButton, 0, 4);
        root.Controls.Add(guestButton, 0, 5);
        Controls.Add(root);
    }

    private static Control WrapWithLabel(string labelText, TextBox textBox)
    {
        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1
        };
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        panel.Controls.Add(new Label
        {
            Text = labelText,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.BottomLeft,
            Font = Theme.Bold()
        }, 0, 0);
        panel.Controls.Add(textBox, 0, 1);
        return panel;
    }

    private void Login()
    {
        var login = _loginTextBox.Text.Trim();
        var password = _passwordTextBox.Text;
        if (login.Length == 0 || password.Length == 0)
        {
            MessageBox.Show("Введите логин и пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var session = Database.Authenticate(login, password);
        if (session is null)
        {
            MessageBox.Show("Пользователь с указанными учетными данными не найден.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        OpenMainForm(session);
    }

    private void OpenMainForm(UserSession session)
    {
        Hide();
        using var form = new MainForm(session);
        form.ShowDialog(this);
        _passwordTextBox.Clear();
        Show();
        _loginTextBox.Focus();
    }
}
