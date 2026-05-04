using StarterApp.Database.Models;

namespace StarterApp.Services;

public interface IApiService
{
    /// <summary>
    /// Sends a login request to the API and retrieves an authentication token.
    /// </summary>
    /// <param name="email">User email address.</param>
    /// <param name="password">User password.</param>
    /// <returns>A token response if successful; otherwise, null.</returns>
    Task<TokenResponse?> getLoginTokenAsync(string email, string password);

    /// <summary>
    /// Sends a registration request to the API and retrieves an authentication token.
    /// </summary>
    /// <param name="firstName">User first name.</param>
    /// <param name="lastName">User last name.</param>
    /// <param name="email">User email address.</param>
    /// <param name="password">User password.</param>
    /// <returns>A token response if successful; otherwise, null.</returns>
    Task<TokenResponse?> getRegisterTokenAsync(string firstName, string lastName, string email, string password);

    /// <summary>
    /// Retrieves a list of item listings from the API.
    /// </summary>
    /// <param name="category">Category filter for listings.</param>
    /// <param name="search">Search term for filtering listings.</param>
    /// <param name="page">Page number to retrieve.</param>
    /// <param name="pageSize">Number of listings per page.</param>
    /// <returns>A listing response containing items if successful; otherwise, null.</returns>
    Task<ListingResponse?> GetListingsAsync(string category, string search, int page, int pageSize);

    /// <summary>
    /// Retrieves detailed information for a specific item.
    /// </summary>
    /// <param name="id">Unique identifier of the item.</param>
    /// <returns>The requested item if found; otherwise, null.</returns>
    Task<Item?> GetItemInfoAsync(int id);

    /// <summary>
    /// Creates a new item listing through the API.
    /// </summary>
    /// <param name="title">Title of the item.</param>
    /// <param name="desc">Description of the item.</param>
    /// <param name="rate">Daily rental rate of the item.</param>
    /// <param name="categoryId">Category identifier for the item.</param>
    /// <returns>True if the listing was created successfully; otherwise, false.</returns>
    Task<bool> CreateItemListingAsync(string title, string desc, decimal rate, int categoryId);

    /// <summary>
    /// Updates an existing item listing through the API.
    /// </summary>
    /// <param name="id">Unique identifier of the item.</param>
    /// <param name="title">Updated item title.</param>
    /// <param name="desc">Updated item description.</param>
    /// <param name="rate">Updated daily rental rate.</param>
    /// <param name="categoryId">Updated category identifier.</param>
    /// <returns>True if the item was updated successfully; otherwise, false.</returns>
    Task<bool> UpdateItemAsync(int id, string title, string desc, decimal rate, int categoryId);
}