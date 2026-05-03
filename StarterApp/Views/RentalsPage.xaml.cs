using StarterApp.Database.Data.Repositories;
using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class RentalsPage : ContentPage
{
    // ---------------------------- VARIBLES ---------------------------------------

    private readonly RentalsViewModel _viewModel;

    // -------------------------------- CONSTRUCTOR -------------------------------------

    public RentalsPage()
    {
        InitializeComponent();

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
        };

        var rentalRepository = new RentalRepository(httpClient);

        _viewModel = new RentalsViewModel(rentalRepository);
        BindingContext = _viewModel;
    }
}