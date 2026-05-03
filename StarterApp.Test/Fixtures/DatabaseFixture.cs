using StarterApp.Database.Models;

namespace StarterApp.Test.Fixtures;

public class DatabaseFixture
{
    public List<Item> Items { get; } = new()
    {
        new Item
        {
            Id = 1,
            ItemTitle = "Bike",
            OwnerId = 10,
            ItemRate = 5,
            CategoryId = 1
        }
    };

    public List<Rental> Rentals { get; } = new()
    {
        new Rental
        {
            Id = 1,
            ItemId = 1,
            ItemTitle = "Bike",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1),
            Status = "requested"
        }
    };
}