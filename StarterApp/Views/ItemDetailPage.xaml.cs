
using StarterApp.Services;
using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class ItemDetailPage : ContentPage, IQueryAttributable
{
    private readonly ItemDetailViewModel _viewModel;

    public ItemDetailPage()
    {
        InitializeComponent();

        var apiService = new ApiService(new HttpClient
        {
            BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
        });

        _viewModel = new ItemDetailViewModel(apiService);
        BindingContext = _viewModel;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idValue))
        {
            var id = Convert.ToInt32(idValue);
            await _viewModel.loadItemInfoAsync(id);
        }
    }
}