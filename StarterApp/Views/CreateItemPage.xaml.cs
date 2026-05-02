using StarterApp.Services;
using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class CreateItemPage : ContentPage, IQueryAttributable
{
    private readonly CreateItemViewModel _viewModel;

    public CreateItemPage()
    {
        InitializeComponent();

        var apiService = new ApiService(new HttpClient
        {
            BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
        });

        var navigationService = new NavigationService();

        _viewModel = new CreateItemViewModel(apiService, navigationService);
        BindingContext = _viewModel;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idValue))
        {
            var id = Convert.ToInt32(idValue);
            await _viewModel.LoadItemAsync(id);
        }
    }
}