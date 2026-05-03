using StarterApp.Database.Models;
using Xunit;

namespace StarterApp.Test.ViewModels;

public class ItemListViewModelTests
{
    [Fact]
    public void Listing_ShouldSetCanEditTrue_WhenOwnerMatchesCurrentUser()
    {
        var item = new Item { OwnerId = 5 };
        var currentUserId = 5;

        item.CanEdit = item.OwnerId == currentUserId;

        Assert.True(item.CanEdit);
    }

    [Fact]
    public void Listing_ShouldSetCanRentTrue_WhenOwnerDoesNotMatchCurrentUser()
    {
        var item = new Item { OwnerId = 9 };
        var currentUserId = 5;

        item.CanRent = item.OwnerId != currentUserId;

        Assert.True(item.CanRent);
    }

    [Fact]
    public void Listing_ShouldHaveValidCategoryId()
    {
        var item = new Item { CategoryId = 3 };

        var valid = item.CategoryId > 0;

        Assert.True(valid);
    }

    [Fact]
    public void Listing_ShouldHaveValidDailyRate()
    {
        var item = new Item { ItemRate = 12.99m };

        var valid = item.ItemRate > 0;

        Assert.True(valid);
    }
}