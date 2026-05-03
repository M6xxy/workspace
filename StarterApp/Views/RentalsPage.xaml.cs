using StarterApp.Services;
using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class RentalsPage : ContentPage
{
    private readonly RentalsViewModel _viewModel;

    public RentalsPage()
    {
        InitializeComponent();

        var apiService = new ApiService(new HttpClient
        {
            BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
        });

        _viewModel = new RentalsViewModel(apiService);
        BindingContext = _viewModel;
    }
}