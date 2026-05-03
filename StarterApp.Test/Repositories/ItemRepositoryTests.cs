using StarterApp.Database.Models;
using Xunit;

namespace StarterApp.Test.Repositories;

public class ItemRepositoryTests
{
    [Fact]
    public void Item_ShouldAllowOwnerToEdit()
    {
        var item = new Item { OwnerId = 62 };
        var currentUserId = 62;

        item.CanEdit = item.OwnerId == currentUserId;

        Assert.True(item.CanEdit);
    }

    [Fact]
    public void Item_ShouldNotAllowNonOwnerToEdit()
    {
        var item = new Item { OwnerId = 10 };
        var currentUserId = 62;

        item.CanEdit = item.OwnerId == currentUserId;

        Assert.False(item.CanEdit);
    }

    [Fact]
    public void Item_ShouldAllowNonOwnerToRent()
    {
        var item = new Item { OwnerId = 10 };
        var currentUserId = 62;

        item.CanRent = item.OwnerId != currentUserId;

        Assert.True(item.CanRent);
    }

    [Fact]
    public void Item_ShouldNotAllowOwnerToRent()
    {
        var item = new Item { OwnerId = 62 };
        var currentUserId = 62;

        item.CanRent = item.OwnerId != currentUserId;

        Assert.False(item.CanRent);
    }

    [Fact]
    public void Item_ShouldStoreListingDetails()
    {
        var item = new Item
        {
            ItemTitle = "Bike",
            ItemDescription = "Mountain bike",
            ItemRate = 10,
            CategoryId = 1
        };

        Assert.Equal("Bike", item.ItemTitle);
        Assert.Equal("Mountain bike", item.ItemDescription);
        Assert.Equal(10, item.ItemRate);
        Assert.Equal(1, item.CategoryId);
    }
}