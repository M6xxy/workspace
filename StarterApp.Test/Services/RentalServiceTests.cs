using StarterApp.Database.Models;
using Xunit;

namespace StarterApp.Test.Services;

public class RentalServiceTests
{
    [Fact]
    public void Rental_ShouldHaveValidDateRange()
    {
        var rental = new Rental
        {
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(2)
        };

        var isValid = rental.EndDate > rental.StartDate;

        Assert.True(isValid);
    }

    [Fact]
    public void Rental_ShouldDetectInvalidDateRange()
    {
        var rental = new Rental
        {
            StartDate = DateTime.Today.AddDays(2),
            EndDate = DateTime.Today
        };

        var isValid = rental.EndDate > rental.StartDate;

        Assert.False(isValid);
    }

    [Fact]
    public void Rental_ShouldStoreBorrowerAndOwner()
    {
        var rental = new Rental
        {
            BorrowerName = "Max",
            OwnerName = "Ryan"
        };

        Assert.Equal("Max", rental.BorrowerName);
        Assert.Equal("Ryan", rental.OwnerName);
    }

    [Fact]
    public void Rental_ShouldStoreTotalPrice()
    {
        var rental = new Rental
        {
            TotalPrice = 25.50m
        };

        Assert.Equal(25.50m, rental.TotalPrice);
    }

    [Fact]
    public void Rental_ShouldStoreStatus()
    {
        var rental = new Rental
        {
            Status = "requested"
        };

        Assert.Equal("requested", rental.Status);
    }
}