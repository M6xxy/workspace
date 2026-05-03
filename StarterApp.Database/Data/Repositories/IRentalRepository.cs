using StarterApp.Database.Models;

namespace StarterApp.Database.Data.Repositories;

public interface IRentalRepository
{
    //Method for getting list of outgoing rentals via API (Returns List<Rental>)
    Task<List<Rental>> GetOutgoingAsync(string token);

    //Method for getting list of incomming rentals via API (Returns List<Rental>)
    Task<List<Rental>> GetIncomingAsync(string token);

    //Method for creating a rental request via API (Returns Bool)
    Task<bool> CreateAsync(Rental rental, string token);
}