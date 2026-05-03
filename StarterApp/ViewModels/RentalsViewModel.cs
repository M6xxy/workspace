using CommunityToolkit.Mvvm.Input;
using StarterApp.Database.Data.Repositories;
using StarterApp.Database.Models;
using System.Collections.ObjectModel;

namespace StarterApp.ViewModels;

public partial class RentalsViewModel : BaseViewModel
{
    // -------------------------- VARIBLES ---------------------------
    private readonly IRentalRepository _rentalRepository;

    public string Title => "Rentals";

    public ObservableCollection<Rental> Rentals { get; } = new();

    public RentalsViewModel(IRentalRepository rentalRepository)
    {
        _rentalRepository = rentalRepository;
    }

    // ------------------------ RELAY COMMANDS ----------------------------

    [RelayCommand] // Load outgoing rental requests via API
    private async Task LoadOutgoingRentalsAsync()
    {
        var token = Preferences.Get("jwt_token", "");

        var rentals = await _rentalRepository.GetOutgoingAsync(token);

        Rentals.Clear();

        foreach (var rental in rentals)
        {
            Rentals.Add(rental);
        }
    }

    [RelayCommand] // Load incoming rental requests via API
    private async Task LoadIncomingRentalsAsync()
    {
        var token = Preferences.Get("jwt_token", "");

        var rentals = await _rentalRepository.GetIncomingAsync(token);

        Rentals.Clear();

        foreach (var rental in rentals)
        {
            Rentals.Add(rental);
        }
    }
}