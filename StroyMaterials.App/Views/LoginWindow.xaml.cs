using System.Windows;
using System.Windows.Input;
using StroyMaterials.App.Models;

namespace StroyMaterials.App.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        LogoImage.Source = ImageTools.LoadLogo(128);
        LoginTextBox.Focus();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        Login();
    }

    private void GuestButton_Click(object sender, RoutedEventArgs e)
    {
        OpenMainWindow(UserSession.Guest);
    }

    private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Login();
        }
    }

    private void Login()
    {
        var login = LoginTextBox.Text.Trim();
        var password = PasswordBox.Password;
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show(this, "Введите логин и пароль.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var session = Database.Authenticate(login, password);
        if (session is null)
        {
            MessageBox.Show(this, "Пользователь с указанными данными не найден.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        OpenMainWindow(session);
    }

    private void OpenMainWindow(UserSession session)
    {
        new MainWindow(session).Show();
        Close();
    }
}
