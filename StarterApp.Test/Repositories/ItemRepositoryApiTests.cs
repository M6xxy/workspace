using System.Net;
using StarterApp.Database.Data.Repositories;
using Xunit;

namespace StarterApp.Test.Repositories;

public class ItemRepositoryApiTests
{
    private static HttpClient CreateClient(string json, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new HttpClient(new FakeHandler(json, statusCode))
        {
            BaseAddress = new Uri("https://test.local")
        };
    }

    [Fact]
    public async Task GetPageAsync_ReturnsItems_WhenApiSucceeds()
    {
        // Arrange
        var json = """
        {
            "items": [
                {
                    "id": 1,
                    "title": "Bike",
                    "description": "Fast bike",
                    "dailyRate": 10,
                    "categoryId": 2,
                    "category": "Sports",
                    "ownerId": 5,
                    "ownerName": "Owner",
                    "isAvailable": true,
                    "createdAt": "2026-05-03T00:00:00Z"
                }
            ]
        }
        """;

        var repository = new ItemRepository(CreateClient(json));

        // Act
        var result = await repository.GetPageAsync(1, 20);

        // Assert
        Assert.Single(result);
        Assert.Equal("Bike", result[0].ItemTitle);
        Assert.Equal(10, result[0].ItemRate);
    }

    [Fact]
    public async Task GetPageAsync_ReturnsEmptyList_WhenApiFails()
    {
        // Arrange
        var repository = new ItemRepository(CreateClient("{}", HttpStatusCode.BadRequest));

        // Act
        var result = await repository.GetPageAsync(1, 20);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsItem_WhenApiSucceeds()
    {
        // Arrange
        var json = """
        {
            "id": 10,
            "title": "Microwave",
            "description": "It cooks",
            "dailyRate": 5,
            "categoryId": 3,
            "ownerId": 7,
            "ownerName": "Ryan",
            "isAvailable": true,
            "createdAt": "2026-05-03T00:00:00Z"
        }
        """;

        var repository = new ItemRepository(CreateClient(json));

        // Act
        var result = await repository.GetByIdAsync(10);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Microwave", result!.ItemTitle);
        Assert.Equal(7, result.OwnerId);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenApiFails()
    {
        // Arrange
        var repository = new ItemRepository(CreateClient("{}", HttpStatusCode.NotFound));

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ReturnsTrue_WhenApiSucceeds()
    {
        // Arrange
        var repository = new ItemRepository(CreateClient("{}", HttpStatusCode.Created));

        var item = new StarterApp.Database.Models.Item
        {
            ItemTitle = "Lamp",
            ItemDescription = "Bright",
            ItemRate = 2,
            CategoryId = 1,
            Latitude = 0,
            Longitude = 0
        };

        // Act
        var result = await repository.CreateAsync(item, "fake-token");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenApiFails()
    {
        // Arrange
        var repository = new ItemRepository(CreateClient("{}", HttpStatusCode.Unauthorized));

        var item = new StarterApp.Database.Models.Item
        {
            ItemTitle = "Lamp",
            ItemDescription = "Bright",
            ItemRate = 2,
            CategoryId = 1
        };

        // Act
        var result = await repository.UpdateAsync(1, item, "bad-token");

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