namespace StarterApp.ViewModels;

using CommunityToolkit.Mvvm.Input;
using StarterApp.Database.Models;
using StarterApp.Services;
using StarterApp.Views;
using System.Collections.ObjectModel;

public partial class ItemsListViewModel : BaseViewModel {
    /// <summary>Service for handling navigation between pages</summary>
    private readonly INavigationService _navigationService;

    //curr page
    private int _currPage = 1;
    private int? _maxPage = null;

    //Page next and prev commandss
    [RelayCommand]
    private async Task NextPageAsync()
    {
        if (_currPage >= _maxPage)
        {

        }
        else {
            _currPage++;
            await LoadListingAsync();
        } 
        
        
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (_currPage <= 1)
            return;

        _currPage--;
        await LoadListingAsync();
    }

    /// @brief Authentication service for managing user authentication
    private readonly IAuthenticationService _authService;

    // Relay command that navigates to listings page
    [RelayCommand]
    private async Task NavigateToListingDetailAsync(Item item)
    {
        await _navigationService.NavigateToAsync(nameof(ItemDetailPage), new Dictionary<string, object> { { "id", item.Id } });
    }
    /// @brief Gets the application title from AppInfo
    /// @return The application name as a string
    public string Title => AppInfo.Name;

    /// @brief Gets the application version from AppInfo
    /// @return The application version string
    public string Version => AppInfo.VersionString;

    /// @brief Gets a placeholder message
    /// @return A message indicating this is a placeholder page
    public string Message => "Page for listings";

    /// @brief Initializes a new instance of the TempViewModel class
    /// @details Default constructor with no initialization logic
    /// 

    private readonly ApiService _apiService;
    public ObservableCollection<Item> Listings { get; } = new();

    public ItemsListViewModel(ApiService apiService, INavigationService navigationService, AuthenticationService authService)
    {
        _navigationService = navigationService;
        _apiService = apiService;
        _authService = authService;

        
        
        _ = LoadListingAsync();
    }

    //Loads listings from api
    public async Task LoadListingAsync()
    {
        try
        {
            //Run api call
            var result = await _apiService.GetListingsAsync("", "", _currPage, 20);

            Listings.Clear();



            //if null
            if (result?.Items == null)
            {
                //Test listing
                Listings.Add(new Item
                {
                    ItemTitle = "Listings Null",
                    ItemDescription = "Listings Null",
                    ItemCategory = "Listings Null",
                    ItemOwnerName = "Listings Null",
                    ItemRate = 20
                });
                return;
            }


            //Get currID
            var currUserId = _authService.CurrentUser?.Id;
            //Add ressults to list
            {
                foreach (var item in result.Items)
                {
                    item.CanEditt = item.OwnerId == currUserId;
                    Listings.Add(item);
                    if (result.Items.Count == 0)
                    {
                        _maxPage = _currPage;
                    }
                }
            }


        }
        catch (Exception ex)
        {
            
        }
    }
}