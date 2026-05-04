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
    // ------------------------------- CONSTRUCTOR ----------------------------
    /// <summary>
    /// Initializes a new instance of the <see cref="RentalsViewModel"/> class.
    /// </summary>
    /// <param name="rentalRepository">Repository used for rental data operations.</param>
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