using System.Windows;
using System.Windows.Input;
using StroyMaterials.App.Models;

namespace StroyMaterials.App.Views;

public partial class RegisterWindow : Window
{
    public UserSession? RegisteredSession { get; private set; }

    public RegisterWindow()
    {
        InitializeComponent();
        FullNameTextBox.Focus();
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        Register();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Register();
        }
    }

    private void Register()
    {
        var fullName = FullNameTextBox.Text.Trim();
        var login = LoginTextBox.Text.Trim().ToLowerInvariant();
        var password = PasswordBox.Password;
        var confirmation = ConfirmPasswordBox.Password;

        if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show(this, "Заполните ФИО, логин и пароль.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length < 2)
        {
            MessageBox.Show(this, "Укажите ФИО не короче двух слов.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (password.Length < 6)
        {
            MessageBox.Show(this, "Пароль должен содержать не менее 6 символов.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (password != confirmation)
        {
            MessageBox.Show(this, "Пароли не совпадают.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            RegisteredSession = Database.RegisterCustomer(fullName, login, password);
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(this, ex.Message, "Регистрация", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        MessageBox.Show(this, "Учетная запись создана.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
        DialogResult = true;
    }
}
