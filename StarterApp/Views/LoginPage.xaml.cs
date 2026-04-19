using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        EmailEntry.Focus();
        EmailEntry.Text = "banana@company.com";
        PasswordEntry.Text = "banana!";
    }
}