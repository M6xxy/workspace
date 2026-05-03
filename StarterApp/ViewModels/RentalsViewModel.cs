using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using StarterApp.Database.Models;
using StarterApp.Services;

namespace StarterApp.ViewModels;

public partial class RentalsViewModel : BaseViewModel
{
    private readonly ApiService _apiService;

    public string Title => "Rentals";

    public ObservableCollection<Rental> Rentals { get; } = new();

    public RentalsViewModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [RelayCommand]
    private async Task LoadOutgoingRentalsAsync()
    {
        var rentals = await _apiService.GetOutgoingRentalsAsync();

        Rentals.Clear();

        foreach (var rental in rentals)
        {
            Rentals.Add(rental);
        }
    }

    [RelayCommand]
    private async Task LoadIncomingRentalsAsync()
    {
        var rentals = await _apiService.GetIncomingRentalsAsync();

        Rentals.Clear();

        foreach (var rental in rentals)
        {
            Rentals.Add(rental);
        }
    }


}