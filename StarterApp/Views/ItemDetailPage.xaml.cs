using StarterApp.Database.Data.Repositories;
using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class ItemDetailPage : ContentPage, IQueryAttributable
{
    // ---------------------------- VARIBLES ---------------------------------------

    private readonly ItemDetailViewModel _viewModel;

    // -------------------------------- CONSTRUCTOR -------------------------------------

    public ItemDetailPage()
    {
        InitializeComponent();

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
        };

        var itemRepository = new ItemRepository(httpClient);

        _viewModel = new ItemDetailViewModel(itemRepository);
        BindingContext = _viewModel;
    }

    // --------------------------------- METHODS -------------------------------------------

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idValue))
        {
            var id = Convert.ToInt32(idValue);
            await _viewModel.LoadItemInfoAsync(id);
        }
    }
}