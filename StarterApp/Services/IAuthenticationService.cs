using StarterApp.Database.Models;

namespace StarterApp.Services;

public interface IAuthenticationService
{
    // -------------------- VARIBLES ---------------------------
    event EventHandler<bool>? AuthenticationStateChanged;
    
    List<string> CurrentUserRoles { get; }
    bool IsAuthenticated { get; }
    User? CurrentUser { get; }

    // ------------------- METHODS ----------------------------------

    /// <summary>
    /// Checks whether the current user has a specific role.
    /// </summary>
    /// <param name="roleName">The role name to check.</param>
    /// <returns>True if the user has the specified role; otherwise, false.</returns>
    bool HasRole(string roleName);
    /// <summary>
    /// Checks whether the current user has at least one of the specified roles.
    /// </summary>
    /// <param name="roleNames">The role names to check.</param>
    /// <returns>True if the user has any of the specified roles; otherwise, false.</returns>
    bool HasAnyRole(params string[] roleNames);
    /// <summary>
    /// Checks whether the current user has all specified roles.
    /// </summary>
    /// <param name="roleNames">The role names to check.</param>
    /// <returns>True if the user has all specified roles; otherwise, false.</returns>
    bool HasAllRoles(params string[] roleNames);

    //Method for logging in via api
    /// <summary>
    /// Logs in a user through the API.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>An authentication result indicating whether login succeeded.</returns>
    Task<AuthenticationResult> LoginAsync(string email, string password);

    //Method for registering via api
    /// <summary>
    /// Registers a new user account through the API.
    /// </summary>
    /// <param name="firstName">The user's first name.</param>
    /// <param name="lastName">The user's last name.</param>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>An authentication result indicating whether registration succeeded.</returns>
    Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password);

    //Method for logging user out
    /// <summary>
    /// Logs out the currently authenticated user.
    /// </summary>
    /// <returns>A completed task once logout has finished.</returns>
    Task LogoutAsync();

    /// <summary>
    /// Attempts to change the current user's password.
    /// </summary>
    /// <param name="currentPassword">The user's current password.</param>
    /// <param name="newPassword">The new password to set.</param>
    /// <returns>True if the password was changed successfully; otherwise, false.</returns>
    Task<bool> ChangePasswordAsync(string currentPassword, string newPassword);
}