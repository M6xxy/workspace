
using StarterApp.Database.Models;


namespace StarterApp.Services;

public class AuthenticationService : IAuthenticationService
{
    // ---------------------------- VARIBLES ---------------------------------------

    private readonly ApiService _apiService;
    private readonly ITokenStorage _tokenStorage;
    public User? CurrentUser => _currentUser;
    public bool IsAuthenticated => !string.IsNullOrWhiteSpace(_currentToken) || _currentUser != null;
    
    private User? _currentUser;

    private string? _currentToken;
    private List<string> _currentUserRoles = new();

    public event EventHandler<bool>? AuthenticationStateChanged;

    public List<string> CurrentUserRoles => _currentUserRoles;

    // --------------------------------- METHODS -------------------------------------------

    // Method for logging in via API
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
            Preferences.Set("jwt_token", tokenResponse.Token);
            Preferences.Set("user_id", tokenResponse.UserId);

            // User
            _currentUser = new User
            {
                Email = email,
                Id = tokenResponse.UserId,
                IsActive = true
            };

            
            // Change auth state
            AuthenticationStateChanged?.Invoke(this, true);
            return new AuthenticationResult(true, "Login successful");
        }
        catch (Exception ex)
        {
            return new AuthenticationResult(false, $"Login failed: {ex.Message}");
        }
    }

    //Method for registering an account via API
    public async Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password)
    {
        try {// Create account
            var tokenResponse = await _apiService.getRegisterTokenAsync(firstName, lastName, email, password);

            return new AuthenticationResult(true, "Registration successful");
        }
        catch (Exception ex)
        {
            return new AuthenticationResult(false, $"Registration failed: {ex.Message}");
        }
    }

    //Method for logging out
    public Task LogoutAsync()
    {
        _currentUser = null;
        _currentUserRoles.Clear();
        AuthenticationStateChanged?.Invoke(this, false);
        Preferences.Clear("jwt_token");
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

// ------------------- Auth Result Classs ----------------------
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



