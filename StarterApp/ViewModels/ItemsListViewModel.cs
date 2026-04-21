namespace StarterApp.ViewModels;

using System.Collections.ObjectModel;
using StarterApp.Database.Models;
public partial class ItemsListViewModel : BaseViewModel {
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
    public ObservableCollection<Item> Listings { get; } = new();

    public ItemsListViewModel()
    {
        Listings.Add(new Item
        {
            ItemTitle = "Drill",
            ItemDescription = "Make some holes",
            ItemRate = 10
        });
    }
}