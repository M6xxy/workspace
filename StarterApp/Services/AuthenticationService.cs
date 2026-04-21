using AndroidX.Browser.Trusted;
using System.Net.Http.Json;
using StarterApp.Database.Models;
using System.Diagnostics;

namespace StarterApp.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ApiService _apiService;
    private readonly ITokenStorage _tokenStorage;
    public User? CurrentUser => _currentUser;
    public bool IsAuthenticated => !string.IsNullOrWhiteSpace(_currentToken) || _currentUser != null;
    
    private User? _currentUser;

    private string? _currentToken;
    private List<string> _currentUserRoles = new();

    public event EventHandler<bool>? AuthenticationStateChanged;

    public List<string> CurrentUserRoles => _currentUserRoles;
    

    public async Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        try
        {
            // GET token
            var tokenResponse = await _apiService.getLoginTokenAsync(email,password);
            
            // If null
            if (tokenResponse == null || string.IsNullOrWhiteSpace(tokenResponse.Token))
            {
                return new AuthenticationResult(false, "Login failed: token missing");
            }

            //Set token
            _currentToken = tokenResponse.Token;
            await _tokenStorage.SaveTokenAsync(tokenResponse.Token);

            // User
            _currentUser = new User
            {
                Email = email,
                IsActive = true
            };

            

            AuthenticationStateChanged?.Invoke(this, true);
            return new AuthenticationResult(true, "Login successful");
        }
        catch (Exception ex)
        {
            return new AuthenticationResult(false, $"Login failed: {ex.Message}");
        }
    }

    public async Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password)
    {
        try {
            var tokenResponse = await _apiService.getRegisterTokenAsync(firstName, lastName, email, password);

            return new AuthenticationResult(true, "Registration successful");
        }
        catch (Exception ex)
        {
            return new AuthenticationResult(false, $"Registration failed: {ex.Message}");
        }
    }

    public Task LogoutAsync()
    {
        _currentUser = null;
        _currentUserRoles.Clear();
        AuthenticationStateChanged?.Invoke(this, false);
        return Task.CompletedTask;
    }

    public bool HasRole(string roleName)
    {
        return _currentUserRoles.Contains(roleName, StringComparer.OrdinalIgnoreCase);
    }

    public bool HasAnyRole(params string[] roleNames)
    {
        return roleNames.Any(role => HasRole(role));
    }

    public bool HasAllRoles(params string[] roleNames)
    {
        return roleNames.All(role => HasRole(role));
    }

    public async Task<bool> ChangePasswordAsync(string currentPassword, string newPassword)
    {
        return false;
    }

    // Constructor
    public AuthenticationService(ApiService apiService, ITokenStorage tokenStorage)
    {
        _apiService = apiService;
    _tokenStorage = tokenStorage;
    }
}

public class AuthenticationResult
{
    public bool IsSuccess { get; }
    public string Message { get; }

    public AuthenticationResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}



