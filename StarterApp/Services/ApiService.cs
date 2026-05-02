using Java.Util;
using StarterApp.Database.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Maui.Storage;
using System.Net.Http.Headers;
using static Android.Icu.Util.LocaleData;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace StarterApp.Services;


public class ApiService
{
    private readonly HttpClient _httpClient;

    public string token;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<TokenResponse?> getLoginTokenAsync(string email, string password)
    {
        try
        {
            //Setup Request
            var request = new
            {
                email,
                password
            };

            //POST for token 
            var response = await _httpClient.PostAsJsonAsync("/auth/token", request);
            var raw = await response.Content.ReadAsStringAsync();

            Debug.WriteLine($"AUTH_DEBUG Status: {(int)response.StatusCode} {response.StatusCode}");
            Debug.WriteLine($"AUTH_DEBUG Body: {raw}");

            // If fails
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return null;
            }
            //Deserialize token
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(
                raw,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            return tokenResponse;
        }
        catch
        {
            return null;
        }
    }

    public async Task<TokenResponse?> getRegisterTokenAsync(string firstName, string lastName, string email, string password)
    {
        try
        {
            //Setup Request
            var request = new
            {
                firstName,
                lastName,
                email,
                password
            };

            // Get result
            var response = await _httpClient.PostAsJsonAsync("/auth/register", request);

            // If fails
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return null;
            }

            //Get response data
            var raw = await response.Content.ReadAsStringAsync();

            //Deserialize token
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(
                raw,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return tokenResponse;
        }
        catch
        {
            return null;
        }

    }

    public async Task<ListingResponse?> GetListingsAsync(string category, string search, int page, int pageSize)
    {
        try
        {

            // Get result
            var response = await _httpClient.GetAsync($"items?page={page}&pageSize={pageSize}");
            var raw = await response.Content.ReadAsStringAsync();

            // If fails
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return null;
            }

            //Deserialize token
            var listingsResponse = JsonSerializer.Deserialize<ListingResponse>(
                raw,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            return listingsResponse;

        }
        catch
        {
            return null;
        }
    }

    [Obsolete]
    public async Task<Item?> GetItemInfoAsync(int id) {
        //Get info
        var response = await _httpClient.GetAsync($"items/{id}");
        var raw = await response.Content.ReadAsStringAsync();
          

        if (!response.IsSuccessStatusCode)
            return null;

        //Deserialize
        var itemResponse = JsonSerializer.Deserialize<Item>(raw,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        return itemResponse;
    }

    [Obsolete]
    public async Task<bool> CreateItemListingAsync(string title, string desc, decimal rate, int categoryId) 
    {
        var token = Preferences.Get("jwt_token", "");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var request = new Item
        {
            ItemTitle = title,
            ItemDescription = desc,
            ItemRate = rate,
            CategoryId = categoryId,
            Latitude = 0,
            Longitude = 0
            
        };

        var response = await _httpClient.PostAsJsonAsync("/items", request);

        // AI SECTION FOR ERROR
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            await Shell.Current.DisplayAlert(
                "Create Listing Failed",
                error,
                "OK");

            return false;
        }

        return response.IsSuccessStatusCode;
    }


}



public class TokenResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public int UserId { get; set; }
}

public class ListingResponse 
{
    public List<Item> Items { get; set; } = new();
}
