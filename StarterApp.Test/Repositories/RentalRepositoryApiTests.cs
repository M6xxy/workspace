using System.Net;
using StarterApp.Database.Data.Repositories;
using StarterApp.Database.Models;
using Xunit;

namespace StarterApp.Test.Repositories;

public class RentalRepositoryApiTests
{
    private static HttpClient CreateClient(string json, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new HttpClient(new FakeHandler(json, statusCode))
        {
            BaseAddress = new Uri("https://test.local")
        };
    }

    [Fact]
    public async Task GetOutgoingAsync_ReturnsRentals_WhenApiSucceeds()
    {
        // Arrange
        var json = """
        {
            "rentals": [
                {
                    "id": 1,
                    "itemId": 10,
                    "itemTitle": "Bike",
                    "ownerId": 5,
                    "ownerName": "Owner",
                    "startDate": "2026-05-03T00:00:00Z",
                    "endDate": "2026-05-04T00:00:00Z",
                    "status": "requested",
                    "totalPrice": 10,
                    "createdAt": "2026-05-03T12:00:00Z"
                }
            ],
            "totalRentals": 1
        }
        """;

        var repository = new RentalRepository(CreateClient(json));

        // Act
        var result = await repository.GetOutgoingAsync("fake-token");

        // Assert
        Assert.Single(result);
        Assert.Equal("Bike", result[0].ItemTitle);
        Assert.Equal("requested", result[0].Status);
    }

    [Fact]
    public async Task GetIncomingAsync_ReturnsRentals_WhenApiSucceeds()
    {
        // Arrange
        var json = """
        {
            "rentals": [
                {
                    "id": 2,
                    "itemId": 20,
                    "itemTitle": "Xbox",
                    "borrowerId": 9,
                    "borrowerName": "Borrower",
                    "ownerId": 5,
                    "ownerName": "Owner",
                    "startDate": "2026-05-05T00:00:00Z",
                    "endDate": "2026-05-06T00:00:00Z",
                    "status": "requested",
                    "totalPrice": 15,
                    "createdAt": "2026-05-03T12:00:00Z"
                }
            ],
            "totalRentals": 1
        }
        """;

        var repository = new RentalRepository(CreateClient(json));

        // Act
        var result = await repository.GetIncomingAsync("fake-token");

        // Assert
        Assert.Single(result);
        Assert.Equal("Xbox", result[0].ItemTitle);
        Assert.Equal("Borrower", result[0].BorrowerName);
    }

    [Fact]
    public async Task GetOutgoingAsync_ReturnsEmptyList_WhenApiFails()
    {
        // Arrange
        var repository = new RentalRepository(CreateClient("{}", HttpStatusCode.Unauthorized));

        // Act
        var result = await repository.GetOutgoingAsync("bad-token");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetIncomingAsync_ReturnsEmptyList_WhenApiFails()
    {
        // Arrange
        var repository = new RentalRepository(CreateClient("{}", HttpStatusCode.Unauthorized));

        // Act
        var result = await repository.GetIncomingAsync("bad-token");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task CreateAsync_ReturnsTrue_WhenApiSucceeds()
    {
        // Arrange
        var repository = new RentalRepository(CreateClient("{}", HttpStatusCode.Created));

        var rental = new Rental
        {
            ItemId = 10,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1)
        };

        // Act
        var result = await repository.CreateAsync(rental, "fake-token");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_ReturnsFalse_WhenApiFails()
    {
        // Arrange
        var repository = new RentalRepository(CreateClient("{}", HttpStatusCode.BadRequest));

        var rental = new Rental
        {
            ItemId = 10,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1)
        };

        // Act
        var result = await repository.CreateAsync(rental, "fake-token");

        // Assert
        Assert.False(result);
    }

    private class FakeHandler : HttpMessageHandler
    {
        private readonly string _json;
        private readonly HttpStatusCode _statusCode;

        public FakeHandler(string json, HttpStatusCode statusCode)
        {
            _json = json;
            _statusCode = statusCode;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_json)
            });
        }
    }
}