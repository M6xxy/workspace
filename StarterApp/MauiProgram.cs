using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StarterApp.Database.Data;
using StarterApp.Database.Data.Repositories;
using StarterApp.Services;
using StarterApp.ViewModels;
using StarterApp.Views;
using System.Diagnostics;

namespace StarterApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddSingleton<ITokenStorage, TokenStorage>();
        
        builder.Services.AddSingleton<IAuthenticationService>(sp =>
{
    //Link for api
        var client = new HttpClient
        {
            BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
        };

        var tokenStorage = sp.GetRequiredService<ITokenStorage>();
        var apiService = new ApiService(client);

        return new AuthenticationService(apiService, tokenStorage);
        });

        builder.Services.AddSingleton<IItemRepository>(sp =>
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
            };

            return new ItemRepository(client);
        });

        builder.Services.AddSingleton<INavigationService, NavigationService>();

        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<App>();

        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddSingleton<RegisterViewModel>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<UserListViewModel>();
        builder.Services.AddTransient<UserListPage>();
        builder.Services.AddTransient<UserDetailPage>();
        builder.Services.AddTransient<UserDetailViewModel>();
        builder.Services.AddSingleton<TempViewModel>();
        builder.Services.AddTransient<TempPage>();
        builder.Services.AddTransient<ItemsListViewModel>();
        builder.Services.AddTransient<ItemsListPage>();
        builder.Services.AddTransient<ItemDetailPage>();
        builder.Services.AddTransient<ItemDetailViewModel>();
        builder.Services.AddTransient<CreateItemPage>();
        builder.Services.AddTransient<CreateItemViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}