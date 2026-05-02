/// @file TempViewModel.cs
/// @brief Temporary placeholder view model
/// @author StarterApp Development Team
/// @date 2025

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StarterApp.Services;
using System.Windows.Input;

namespace StarterApp.ViewModels;

/// @brief Temporary view model for placeholder pages
/// @details Simple view model that displays basic application information
/// @note This is a placeholder implementation for temporary pages
public partial class CreateItemViewModel : BaseViewModel
{
    private readonly ApiService _apiService;
    private readonly INavigationService _navigationService;

    private int editingItemId = -1;

    /// @brief Gets the application title from AppInfo
    /// @return The application name as a string
    [ObservableProperty]
    private string title = "Create Listing";

    /// @brief Gets the application version from AppInfo
    /// @return The application version string
    public string Version => AppInfo.VersionString;


    /// @brief Initializes a new instance of the TempViewModel class
    /// @details Default constructor with no initialization logic
    /// 

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
    public CreateItemViewModel(
        ApiService apiService,
        INavigationService navigationService)
    {
        _apiService = apiService;
        _navigationService = navigationService;
    }

    [RelayCommand]
    [Obsolete]
    private async Task CreateItemAsync()
    {
        if (CategoryId <= 0)
        {
            await Shell.Current.DisplayAlert("Invalid Category", "Category ID must be greater than 0.", "OK");
            return;
        }

        bool success;

        if (editingItemId > 0)
        {
            success = await _apiService.UpdateItemAsync(
                editingItemId,
                ItemTitle,
                ItemDescription,
                ItemRate,
                CategoryId);
        }
        else
        {
            success = await _apiService.CreateItemListingAsync(
                ItemTitle,
                ItemDescription,
                ItemRate,
                CategoryId);
        }

        if (success)
        {
            await Shell.Current.DisplayAlert(
                "Success",
                editingItemId > 0 ? "Listing updated" : "Listing created",
                "OK");

            await _navigationService.NavigateBackAsync();
        }
        else
        {
            await Shell.Current.DisplayAlert(
                "Error",
                editingItemId > 0 ? "Failed to update listing" : "Failed to create listing",
                "OK");
        }
    }

    [Obsolete]
    internal async Task LoadItemAsync(int id)
    {
        editingItemId = id;
        Title = "Edit Listing";

        var item = await _apiService.GetItemInfoAsync(id);

        if (item == null)
        {
            await Shell.Current.DisplayAlert("Error", "Could not load listing", "OK");
            return;
        }

        ItemTitle = item.ItemTitle;
        ItemDescription = item.ItemDescription;
        ItemRate = item.ItemRate;
        CategoryId = item.CategoryId;
        Latitude = item.Latitude ?? 0;
        Longitude = item.Longitude ?? 0;
    }
}