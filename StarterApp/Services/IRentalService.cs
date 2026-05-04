using StarterApp.Database.Models;

namespace StarterApp.Services
{
    public interface IRentalService
    {
        /// <summary>
        /// Retrieves a list of outgoing rental requests for the current user.
        /// </summary>
        /// <returns>A list of outgoing rentals.</returns>
        Task<List<Rental>> GetOutgoingRentalsAsync();

        /// <summary>
        /// Retrieves a list of incoming rental requests for the current user.
        /// </summary>
        /// <returns>A list of incoming rentals.</returns>
        Task<List<Rental>> GetIncomingRentalsAsync();

        /// <summary>
        /// Creates a new rental request for an item.
        /// </summary>
        /// <param name="itemId">The unique identifier of the item to rent.</param>
        /// <param name="startDate">The rental start date.</param>
        /// <param name="endDate">The rental end date.</param>
        /// <returns>True if the rental request was created successfully; otherwise, false.</returns>
        Task<bool> CreateRentalRequestAsync(
            int itemId,
            DateTime startDate,
            DateTime endDate);
    }
}