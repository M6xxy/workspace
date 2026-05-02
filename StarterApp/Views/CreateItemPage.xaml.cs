using StarterApp.Services;
using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class CreateItemPage : ContentPage
{
	public CreateItemPage()
    {
        InitializeComponent();

        var apiService = new ApiService(new HttpClient
        {
            BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
        });

        var navigationService = new NavigationService();

        BindingContext = new CreateItemViewModel(
            apiService,
            navigationService);
    }
}