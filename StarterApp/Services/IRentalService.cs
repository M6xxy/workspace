using StarterApp.Database.Models;

namespace StarterApp.Services
{
    public interface IRentalService
    {
        Task<List<Rental>> GetOutgoingRentalsAsync();

        Task<List<Rental>> GetIncomingRentalsAsync();

        Task<bool> CreateRentalRequestAsync(
            int itemId,
            DateTime startDate,
            DateTime endDate);
    }
}