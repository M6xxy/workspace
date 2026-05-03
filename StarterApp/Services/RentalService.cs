using Org.Apache.Http.Client;
using StarterApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Net.Http.Json;

namespace StarterApp.Services
{
    public class RentalService : IRentalService
    {
        // ----------------------------- VARIBLES ---------------------------------------
        private readonly HttpClient _httpClient;

        // -------------------------------- CONSTRUCTOR -------------------------------------

        public RentalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // --------------------------------- METHODS ---------------------------------------

        // Method for getting outgoing rental list via API
        public async Task<List<Rental>> GetOutgoingRentalsAsync()
        {
            //JWT Token
            var token = Preferences.Get("jwt_token", "");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            //Response
            var response = await _httpClient.GetAsync("/rentals/outgoing");
            var raw = await response.Content.ReadAsStringAsync();

            //If fails
            if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Rentals Error", raw, "OK");
                return new List<Rental>();
            }

            //Deserialize
            var result = JsonSerializer.Deserialize<RentalResponse>(
                raw,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result?.Rentals ?? new List<Rental>();
        }

        // Method to get incomming rental list via API
        public async Task<List<Rental>> GetIncomingRentalsAsync()
        {
            //JWT TOKEN
            var token = Preferences.Get("jwt_token", "");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            //Responsse
            var response = await _httpClient.GetAsync("/rentals/incoming");
            var raw = await response.Content.ReadAsStringAsync();

            //If fails
            if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Rentals Error", raw, "OK");
                return new List<Rental>();
            }

            //Deserialize
            var result = JsonSerializer.Deserialize<RentalResponse>(
                raw,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result?.Rentals ?? new List<Rental>();
        }

        //Method for creating a rental request
        public async Task<bool> CreateRentalRequestAsync(int itemId, DateTime startDate, DateTime endDate)
        {
            //JWT Token
            var token = Preferences.Get("jwt_token", "");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            //Request
            var request = new
            {
                itemId,
                startDate = startDate.ToString("yyyy-MM-dd"),
                endDate = endDate.ToString("yyyy-MM-dd")
            };

            //Response
            var response = await _httpClient.PostAsJsonAsync("/rentals", request);

            //If fails
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                await Shell.Current.DisplayAlert(
                    "Rental Request Failed",
                    error,
                    "OK");

                return false;
            }

            return true;
        }
    }
}

// ----------------------------- Response CLASSES ---------------------------------------

public class RentalResponse
{
    public List<Rental> Rentals { get; set; } = new();
    public int TotalRentals { get; set; }
}