namespace StarterApp.ViewModels;

using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using StarterApp.Database.Data.Repositories;
using StarterApp.Database.Models;
using StarterApp.Services;
using StarterApp.Views;
using System.Collections.ObjectModel;

public partial class ItemsListViewModel : BaseViewModel
{
    // ----------------------- VARIBLES ---------------------------------
    private readonly INavigationService _navigationService;
    private readonly IAuthenticationService _authService;
    private readonly IItemRepository _itemRepository;
    private readonly IRentalRepository _rentalRepository;

    private int _currPage = 1;
    private int? _maxPage = null;

    public string Title => AppInfo.Name;
    public string Version => AppInfo.VersionString;
    public string Message => "Page for listings";

    public ObservableCollection<Item> Listings { get; } = new();
    // ------------------------ CONSTRUCTOR -------------------------
    public ItemsListViewModel(
        IItemRepository itemRepository,
        IRentalRepository rentalRepository,
        INavigationService navigationService,
        IAuthenticationService authService)
    {
        _itemRepository = itemRepository;
        _rentalRepository = rentalRepository;
        _navigationService = navigationService;
        _authService = authService;
    }

    // -------------------- RELAY COMMANDS ---------------------------
    [RelayCommand] // Open next page
    private async Task NextPageAsync()
    {
        if (_maxPage != null && _currPage >= _maxPage)
            return;

        _currPage++;
        await LoadListingAsync();
    }

    [RelayCommand] // Open previous page
    private async Task PreviousPageAsync()
    {
        if (_currPage <= 1)
            return;

        _currPage--;
        await LoadListingAsync();
    }

    [RelayCommand] // Open Detail Page
    private async Task NavigateToListingDetailAsync(Item item)
    {
        if (item == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?id={item.Id}");
    }

    [RelayCommand] // Open Edit Lisssting Page
    private async Task EditListingAsync(Item item)
    {
        if (item == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(CreateItemPage)}?id={item.Id}");
    }

    [RelayCommand] // Send Rental Request via API
    private async Task RentItemAsync(Item item)
    {
        if (item == null)
            return;

        var token = Preferences.Get("jwt_token", "");

        var rental = new Rental
        {
            ItemId = item.Id,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1)
        };

        var success = await _rentalRepository.CreateAsync(rental, token);

        if (success)
        {
            await Shell.Current.DisplayAlert(
                "Success",
                "Rental request sent",
                "OK");
        }
    }

    // ------------------------ METHODS --------------------------------------
    
    // Load listing content via API
    public async Task LoadListingAsync()
    {
        try
        {
            var items = await _itemRepository.GetPageAsync(_currPage, 20);

            Listings.Clear();

            if (items == null || items.Count == 0)
            {
                _maxPage = _currPage;
                return;
            }

            var currUserId = Preferences.Get("user_id", -1);

            foreach (var item in items)
            {
                item.CanEdit = item.OwnerId == currUserId;
                item.CanRent = item.OwnerId != currUserId;

                Listings.Add(item);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert(
                "Listings Error",
                ex.Message,
                "OK");
        }
    }
}