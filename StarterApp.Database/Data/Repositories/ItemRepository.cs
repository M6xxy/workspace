using StarterApp.Database.Models;
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

    //Method for getting page content, Asynchronous returns a list of page content via API (Returns List<Item>)
    public async Task<List<Item>> GetPageAsync(int page, int pageSize)
    {
        //Request
        var response = await _httpClient.GetAsync($"/items?page={page}&pageSize={pageSize}");
        var raw = await response.Content.ReadAsStringAsync();

        //Processing of response
        if (!response.IsSuccessStatusCode)
            return new List<Item>();

        //Deserialization
        var result = JsonSerializer.Deserialize<ListingResponse>(
            raw,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result?.Items ?? new List<Item>();
    }

    //Method for getting an item by ID, Asynchrous returns a Item or Null via API (Returns <Item> || Null)
    public async Task<Item?> GetByIdAsync(int id)
    {
        //Request
        var response = await _httpClient.GetAsync($"/items/{id}");
        var raw = await response.Content.ReadAsStringAsync();

        //Processing of response
        if (!response.IsSuccessStatusCode)
            return null;

        //Deserialization
        return JsonSerializer.Deserialize<Item>(
            raw,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    //Method for creating a an item Asynchrous via API (Returns bool)
    public async Task<bool> CreateAsync(Item item, string token)
    {
        //API JWT
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        //Request
        var response = await _httpClient.PostAsJsonAsync("/items", item);

        return response.IsSuccessStatusCode;
    }

    //Method for updating existing item info via API (Returns bool)
    public async Task<bool> UpdateAsync(int id, Item item, string token)
    {
        //API JWT
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        //Request
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
// Response for JSON deserialization
public class ListingResponse
{
    public List<Item> Items { get; set; } = new();
}