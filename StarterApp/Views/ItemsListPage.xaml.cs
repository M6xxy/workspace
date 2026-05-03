using StarterApp.Database.Data.Repositories;
using StarterApp.Services;
using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class ItemsListPage : ContentPage
{
    // ---------------------------- VARIBLES ---------------------------------------

    private readonly ItemsListViewModel _viewModel;

    // -------------------------------- CONSTRUCTOR -------------------------------------

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

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
        };

        var itemRepository = new ItemRepository(httpClient);
        var rentalRepository = new RentalRepository(httpClient);

        _viewModel = new ItemsListViewModel(
            itemRepository,
            rentalRepository,
            navigationService,
            authService);

        BindingContext = _viewModel;
    }

    // --------------------------------- METHODS -------------------------------------------


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadListingAsync();
    }
}