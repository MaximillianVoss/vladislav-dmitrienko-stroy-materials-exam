using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StroyMaterials.App.Models;
using StroyMaterials.App.UI;

namespace StroyMaterials.App.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        LogoImage.Source = ImageTools.LoadLogo(128);
#if DEBUG
        LoadDebugAccounts();
#endif
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

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new RegisterWindow
        {
            Owner = this
        };

        if (window.ShowDialog() == true && window.RegisteredSession is not null)
        {
            OpenMainWindow(window.RegisteredSession);
        }
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

#if DEBUG
    private void LoadDebugAccounts()
    {
        var accounts = Database.GetDebugLoginAccounts();
        if (accounts.Count == 0)
        {
            return;
        }

        DebugAccountsList.ItemsSource = accounts;
        DebugAccountsPanel.Visibility = Visibility.Visible;
        Width = 1080;
        Height = 820;
        MinHeight = 760;
    }

#endif

    private void DebugAccountButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: DebugLoginAccountView account })
        {
            return;
        }

        LoginTextBox.Text = account.Login;
        PasswordBox.Password = account.Password;
        Login();
    }

    private void OpenMainWindow(UserSession session)
    {
        new MainWindow(session).Show();
        Close();
    }
}
