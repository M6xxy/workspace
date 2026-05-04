using StarterApp.Database.Models;

namespace StarterApp.Services;

public class AuthResult
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public User? User { get; set; }
    public List<string> Roles { get; set; } = new();

    /// <summary>
    /// Creates a successful authentication result.
    /// </summary>
    /// <param name="user">The authenticated user.</param>
    /// <param name="roles">The roles assigned to the authenticated user.</param>
    /// <returns>An authentication result representing a successful operation.</returns>
    public static AuthResult Success(User user, List<string> roles)
    {
        return new AuthResult
        {
            IsSuccess = true,
            User = user,
            Roles = roles
        };
    }

    /// <summary>
    /// Creates a failed authentication result.
    /// </summary>
    /// <param name="errorMessage">The error message describing the failure.</param>
    /// <returns>An authentication result representing a failed operation.</returns>
    public static AuthResult Failure(string errorMessage)
    {
        return new AuthResult
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }
}