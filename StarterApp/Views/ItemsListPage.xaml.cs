using AndroidX.Lifecycle;
using StarterApp.Services;
using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class ItemsListPage : ContentPage
{
    public ItemsListPage()
    {
        InitializeComponent();

        var apiService = new ApiService(new HttpClient
        {
            BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
        });

        var navigationService = new NavigationService();

        BindingContext = new ItemsListViewModel(apiService,navigationService);
    }
}