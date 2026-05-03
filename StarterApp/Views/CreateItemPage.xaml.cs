using StarterApp.Database.Data.Repositories;
using StarterApp.Services;
using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class CreateItemPage : ContentPage, IQueryAttributable
{
    // ---------------------------- VARIBLES ---------------------------------------
    
    private readonly CreateItemViewModel _viewModel;

    // -------------------------------- CONSTRUCTOR -------------------------------------
    public CreateItemPage()
    {
        InitializeComponent();

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
        };

        var itemRepository = new ItemRepository(httpClient);
        var navigationService = new NavigationService();

        _viewModel = new CreateItemViewModel(itemRepository, navigationService);
        BindingContext = _viewModel;
    }

    // --------------------------------- METHODS -------------------------------------------

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idValue))
        {
            var id = Convert.ToInt32(idValue);
            await _viewModel.LoadItemAsync(id);
        }
    }
}