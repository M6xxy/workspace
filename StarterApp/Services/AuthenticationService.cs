
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
    /// <summary>
    /// Logs in a user through the API and stores the returned authentication token.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>An authentication result indicating whether login succeeded.</returns>
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
    /// <summary>
    /// Registers a new user account through the API.
    /// </summary>
    /// <param name="firstName">The user's first name.</param>
    /// <param name="lastName">The user's last name.</param>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>An authentication result indicating whether registration succeeded.</returns>
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
    /// <summary>
    /// Logs out the current user and clears stored authentication state.
    /// </summary>
    /// <returns>A completed task once logout is finished.</returns>
    public Task LogoutAsync()
    {
        _currentUser = null;
        _currentUserRoles.Clear();
        AuthenticationStateChanged?.Invoke(this, false);
        Preferences.Clear("jwt_token");
        return Task.CompletedTask;
    }


    /// <summary>
    /// Checks whether the current user has a specific role.
    /// </summary>
    /// <param name="roleName">The role name to check.</param>
    /// <returns>True if the user has the role; otherwise, false.</returns>
    public bool HasRole(string roleName)
    {
        return _currentUserRoles.Contains(roleName, StringComparer.OrdinalIgnoreCase);
    }


    /// <summary>
    /// Checks whether the current user has at least one of the specified roles.
    /// </summary>
    /// <param name="roleNames">The role names to check.</param>
    /// <returns>True if the user has any of the specified roles; otherwise, false.</returns>
    public bool HasAnyRole(params string[] roleNames)
    {
        return roleNames.Any(role => HasRole(role));
    }

    /// <summary>
    /// Checks whether the current user has all of the specified roles.
    /// </summary>
    /// <param name="roleNames">The role names to check.</param>
    /// <returns>True if the user has all specified roles; otherwise, false.</returns>
    public bool HasAllRoles(params string[] roleNames)
    {
        return roleNames.All(role => HasRole(role));
    }

    /// <summary>
    /// Attempts to change the current user's password.
    /// </summary>
    /// <param name="currentPassword">The user's current password.</param>
    /// <param name="newPassword">The new password to set.</param>
    /// <returns>True if the password was changed successfully; otherwise, false.</returns>
    public async Task<bool> ChangePasswordAsync(string currentPassword, string newPassword)
    {
        return false;
    }

    // Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
    /// </summary>
    /// <param name="apiService">The API service used for authentication requests.</param>
    /// <param name="tokenStorage">The token storage service used to save authentication tokens.</param>
    public AuthenticationService(ApiService apiService, ITokenStorage tokenStorage)
    {
        _apiService = apiService;
    _tokenStorage = tokenStorage;
    }
}

// ------------------- Auth Result Classs ----------------------
/// <summary>
/// Initializes a new instance of the <see cref="AuthenticationResult"/> class.
/// </summary>
/// <param name="isSuccess">Whether the authentication operation succeeded.</param>
/// <param name="message">The result message describing the authentication outcome.</param>
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



