using StarterApp.Database.Models;

namespace StarterApp.Services;

public interface IAuthenticationService
{
    // -------------------- VARIBLES ---------------------------
    event EventHandler<bool>? AuthenticationStateChanged;
    
    List<string> CurrentUserRoles { get; }
    bool IsAuthenticated { get; }
    User? CurrentUser { get; }
    bool HasRole(string roleName);
    bool HasAnyRole(params string[] roleNames);
    bool HasAllRoles(params string[] roleNames);


    // ------------------- METHODS ----------------------------------

    //Method for logging in via api
    Task<AuthenticationResult> LoginAsync(string email, string password);

    //Method for registering via api
    Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password);

    //Method for logging user out
    Task LogoutAsync();
    

    Task<bool> ChangePasswordAsync(string currentPassword, string newPassword);
}