using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StarterApp.Database.Data.Repositories;
using StarterApp.Database.Models;
using StarterApp.Services;

namespace StarterApp.ViewModels;

public partial class CreateItemViewModel : BaseViewModel
{
    private readonly IItemRepository _itemRepository;
    private readonly INavigationService _navigationService;

    private int editingItemId = -1;
    // ---------------------- OBSERVABLE PROPERTIES ------------------------------
    [ObservableProperty]
    private string title = "Create Listing";

    [ObservableProperty]
    private string itemTitle = "";

    [ObservableProperty]
    private string itemDescription = "";

    [ObservableProperty]
    private decimal itemRate;

    [ObservableProperty]
    private int categoryId;

    [ObservableProperty]
    private decimal latitude;

    [ObservableProperty]
    private decimal longitude;

    // ------------------ CONSTRUCTOR ----------------------
    public CreateItemViewModel(
        IItemRepository itemRepository,
        INavigationService navigationService)
    {
        _itemRepository = itemRepository;
        _navigationService = navigationService;
    }
    // ------------------ METHODS -----------------------------

    // METHOD FOR LOADING ITEM BY ID FOR EDITING (ASYNC)
    public async Task LoadItemAsync(int id)
    {
        editingItemId = id;
        Title = "Edit Listing";

        var item = await _itemRepository.GetByIdAsync(id);

        if (item == null)
            return;

        ItemTitle = item.ItemTitle;
        ItemDescription = item.ItemDescription;
        ItemRate = item.ItemRate;
        CategoryId = item.CategoryId;
        Latitude = item.Latitude ?? 0;
        Longitude = item.Longitude ?? 0;
    }

    // ------------------ XAML COMMANDS --------------------------------

    [RelayCommand]
    private async Task CreateItemAsync()
    {
        // MAKE SURE CATEGORY IS VALID
        if (CategoryId <= 0)
        {
            await Shell.Current.DisplayAlert("Invalid Category", "Category ID must be greater than 0.", "OK");
            return;
        }

        // GET JWT TOKEN
        var token = Preferences.Get("jwt_token", "");

        //CREATE NEW ITEM OBJECT TO UPLOAD TO API
        var item = new Item
        {
            ItemTitle = ItemTitle,
            ItemDescription = ItemDescription,
            ItemRate = ItemRate,
            CategoryId = CategoryId,
            Latitude = Latitude,
            Longitude = Longitude
        };

        // STATUS FOR API CALL
        bool success;
        
        // IF EDITING | ELSE CREATE NEW ITEM
        if (editingItemId > 0)
        {
            success = await _itemRepository.UpdateAsync(editingItemId, item, token);
        }
        else
        {
            success = await _itemRepository.CreateAsync(item, token);
        }

        // API SUCCESS MESSAGE
        if (success)
        {
            await Shell.Current.DisplayAlert(
                "Success",
                editingItemId > 0 ? "Listing updated" : "Listing created",
                "OK");

            await _navigationService.NavigateBackAsync();
        }
        else // FAILED
        {
            await Shell.Current.DisplayAlert("Error", "Operation failed", "OK");
        }
    }
}