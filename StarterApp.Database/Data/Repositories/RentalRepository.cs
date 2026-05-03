using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using StarterApp.Database.Models;

namespace StarterApp.Database.Data.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly HttpClient _httpClient;

    public RentalRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    //Method for getting list of outgoing rentals via API (Returns List<Rental>)
    public async Task<List<Rental>> GetOutgoingAsync(string token)
    {
        //API JWT
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        //Request
        var response = await _httpClient.GetAsync("/rentals/outgoing");
        var raw = await response.Content.ReadAsStringAsync();
        
        //Handle Request Statuss
        if (!response.IsSuccessStatusCode)
            return new List<Rental>();

        //Deserialize Response
        var result = JsonSerializer.Deserialize<RentalResponse>(
            raw,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return result?.Rentals ?? new List<Rental>();
    }

    //Method for getting list of incomming rentals via API (Returns List<Rental>)
    public async Task<List<Rental>> GetIncomingAsync(string token)
    {
        //API JWT
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        //Request
        var response = await _httpClient.GetAsync("/rentals/incoming");
        var raw = await response.Content.ReadAsStringAsync();

        //Handle Request Status
        if (!response.IsSuccessStatusCode)
            return new List<Rental>();

        //Deserialize Responsse
        var result = JsonSerializer.Deserialize<RentalResponse>(
            raw,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return result?.Rentals ?? new List<Rental>();
    }

    //Method for creating a rental request via API (Returns Bool)
    public async Task<bool> CreateAsync(Rental rental, string token)
    {
        //API JWT
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        //Request
        var request = new
        {
            itemId = rental.ItemId,
            startDate = rental.StartDate.ToString("yyyy-MM-dd"),
            endDate = rental.EndDate.ToString("yyyy-MM-dd")
        };

        //Response
        var response = await _httpClient.PostAsJsonAsync("/rentals", request);

        return response.IsSuccessStatusCode;
    }
}

// Response for JSON deserialization
public class RentalResponse
{
    public List<Rental> Rentals { get; set; } = new();
    public int TotalRentals { get; set; }
}