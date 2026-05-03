using StarterApp.Database.Models;
using StarterApp.Repositories;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace StarterApp.Database.Data.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly HttpClient _httpClient;

    public ItemRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Item>> GetPageAsync(int page, int pageSize)
    {
        var response = await _httpClient.GetAsync($"/items?page={page}&pageSize={pageSize}");
        var raw = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return new List<Item>();

        var result = JsonSerializer.Deserialize<ListingResponse>(
            raw,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result?.Items ?? new List<Item>();
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/items/{id}");
        var raw = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return null;

        return JsonSerializer.Deserialize<Item>(
            raw,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<bool> CreateAsync(Item item, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync("/items", item);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(int id, Item item, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var request = new
        {
            title = item.ItemTitle,
            description = item.ItemDescription,
            dailyRate = item.ItemRate,
            categoryId = item.CategoryId,
            latitude = item.Latitude ?? 0,
            longitude = item.Longitude ?? 0
        };

        var response = await _httpClient.PutAsJsonAsync($"/items/{id}", request);
        return response.IsSuccessStatusCode;
    }
}

public class ListingResponse
{
    public List<Item> Items { get; set; } = new();
}