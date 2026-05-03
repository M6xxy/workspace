using StarterApp.Database.Models;
using Xunit;

namespace StarterApp.Test.Models;

public class RoleConstraintTests
{
    [Fact]
    public void AdminRole_ShouldBeCorrect()
    {
        // Arrange
        var expected = "Admin";

        // Act
        var actual = RoleConstants.Admin;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void OrdinaryUserRole_ShouldBeCorrect()
    {
        // Arrange
        var expected = "OrdinaryUser";

        // Act
        var actual = RoleConstants.OrdinaryUser;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SpecialUserRole_ShouldBeCorrect()
    {
        // Arrange
        var expected = "SpecialUser";

        // Act
        var actual = RoleConstants.SpecialUser;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AllRoles_ShouldContainThreeRoles()
    {
        // Arrange / Act
        var roles = RoleConstants.AllRoles;

        // Assert
        Assert.Equal(3, roles.Length);
    }

    [Fact]
    public void AllRoles_ShouldContainAdmin()
    {
        // Arrange / Act
        var roles = RoleConstants.AllRoles;

        // Assert
        Assert.Contains(RoleConstants.Admin, roles);
    }

    [Fact]
    public void AllRoles_ShouldContainOrdinaryUser()
    {
        // Arrange / Act
        var roles = RoleConstants.AllRoles;

        // Assert
        Assert.Contains(RoleConstants.OrdinaryUser, roles);
    }

    [Fact]
    public void AllRoles_ShouldContainSpecialUser()
    {
        // Arrange / Act
        var roles = RoleConstants.AllRoles;

        // Assert
        Assert.Contains(RoleConstants.SpecialUser, roles);
    }
}