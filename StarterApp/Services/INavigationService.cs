namespace StarterApp.Services;

public interface INavigationService
{
    /// <summary>
    /// Navigates to a specified route.
    /// </summary>
    /// <param name="route">The route to navigate to.</param>
    /// <returns>A task representing the asynchronous navigation operation.</returns>
    Task NavigateToAsync(string route);

    /// <summary>
    /// Navigates to a specified route with navigation parameters.
    /// </summary>
    /// <param name="route">The route to navigate to.</param>
    /// <param name="parameters">The parameters passed during navigation.</param>
    /// <returns>A task representing the asynchronous navigation operation.</returns>
    Task NavigateToAsync(string route, Dictionary<string, object> parameters);

    /// <summary>
    /// Navigates back to the previous page.
    /// </summary>
    /// <returns>A task representing the asynchronous navigation operation.</returns>
    Task NavigateBackAsync();

    /// <summary>
    /// Navigates to the root page of the application.
    /// </summary>
    /// <returns>A task representing the asynchronous navigation operation.</returns>
    Task NavigateToRootAsync();

    /// <summary>
    /// Removes all pages from the navigation stack except the root page.
    /// </summary>
    /// <returns>A task representing the asynchronous navigation operation.</returns>
    Task PopToRootAsync();
}