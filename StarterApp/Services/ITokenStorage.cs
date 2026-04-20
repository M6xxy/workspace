namespace StarterApp.Services;

// Interface for Saving, Getting and clearing token Async
public interface ITokenStorage
{
    Task SaveTokenAsync(string token);
    Task<string?> GetTokenAsync();
    Task ClearTokenAsync();
}

