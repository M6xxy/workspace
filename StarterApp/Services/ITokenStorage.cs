namespace StarterApp.Services;

// Interface for Saving, Getting and clearing token Async
public interface ITokenStorage
{
    /// <summary>
    /// Saves an authentication token asynchronously.
    /// </summary>
    /// <param name="token">The authentication token to save.</param>
    /// <returns>A task representing the asynchronous save operation.</returns>
    Task SaveTokenAsync(string token);

    /// <summary>
    /// Retrieves the stored authentication token asynchronously.
    /// </summary>
    /// <returns>The stored authentication token if found; otherwise, null.</returns>
    Task<string?> GetTokenAsync();

    /// <summary>
    /// Clears the stored authentication token asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous clear operation.</returns>
    Task ClearTokenAsync();
}

