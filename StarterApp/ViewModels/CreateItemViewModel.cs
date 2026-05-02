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

    /// @brief Gets the application title from AppInfo
    /// @return The application name as a string
    public string Title => "Create Listing";
    
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
        var token = Preferences.Get("jwt_token", "");

        var success = await _apiService.CreateItemListingAsync(
            ItemTitle,
            ItemDescription,
            ItemRate,
            CategoryId
        );

        if (success)
        {
            await Shell.Current.DisplayAlert(
                "Success",
                "Listing created",
                "OK");

            await _navigationService.NavigateBackAsync();
        }
        else
        {
            await Shell.Current.DisplayAlert(
                "Error",
                "Failed to create listing",
                "OK");
        }
    }
}