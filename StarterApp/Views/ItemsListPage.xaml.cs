using StarterApp.Services;
using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class ItemsListPage : ContentPage
{
    private readonly ItemsListViewModel _viewModel;

    public ItemsListPage()
    {
        InitializeComponent();

        var apiService = new ApiService(new HttpClient
        {
            BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
        });

        var navigationService = new NavigationService();
        var tokenService = new TokenStorage();

        var authService = new AuthenticationService(
            apiService,
            tokenService);

        _viewModel = new ItemsListViewModel(
            apiService,
            navigationService,
            authService);

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadListingAsync();
    }
}