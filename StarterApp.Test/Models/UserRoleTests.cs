using StarterApp.Database.Models;
using Xunit;

namespace StarterApp.Test.Models;

public class UserRoleTests
{
    [Fact]
    public void DefaultConstructor_ShouldSetDefaults()
    {
        var userRole = new UserRole();

        Assert.True(userRole.IsActive);
        Assert.NotNull(userRole.CreatedAt);
        Assert.NotNull(userRole.UpdatedAt);
        Assert.Null(userRole.DeletedAt);
    }

    [Fact]
    public void Constructor_ShouldSetUserIdAndRoleId()
    {
        var userRole = new UserRole(5, 2);

        Assert.Equal(5, userRole.UserId);
        Assert.Equal(2, userRole.RoleId);
        Assert.True(userRole.IsActive);
    }

    [Fact]
    public void UpdateTimestamps_ShouldChangeUpdatedAt()
    {
        var userRole = new UserRole();
        var oldUpdatedAt = userRole.UpdatedAt;

        Thread.Sleep(5);
        userRole.UpdateTimestamps();

        Assert.True(userRole.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void MarkAsDeleted_ShouldSetDeletedAtAndDeactivate()
    {
        var userRole = new UserRole();

        userRole.MarkAsDeleted();

        Assert.NotNull(userRole.DeletedAt);
        Assert.False(userRole.IsActive);
    }

    [Fact]
    public void Restore_ShouldClearDeletedAtAndReactivate()
    {
        var userRole = new UserRole();
        userRole.MarkAsDeleted();

        userRole.Restore();

        Assert.Null(userRole.DeletedAt);
        Assert.True(userRole.IsActive);
        Assert.NotNull(userRole.UpdatedAt);
    }

    [Fact]
    public void ToString_ShouldContainImportantValues()
    {
        var userRole = new UserRole(10, 20)
        {
            Id = 1
        };

        var text = userRole.ToString();

        Assert.Contains("Id: 1", text);
        Assert.Contains("UserId: 10", text);
        Assert.Contains("RoleId: 20", text);
        Assert.Contains("IsActive: True", text);
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForSameValues()
    {
        var created = DateTime.UtcNow;

        var first = new UserRole
        {
            Id = 1,
            UserId = 5,
            RoleId = 2,
            CreatedAt = created,
            UpdatedAt = created,
            DeletedAt = null,
            IsActive = true
        };

        var second = new UserRole
        {
            Id = 1,
            UserId = 5,
            RoleId = 2,
            CreatedAt = created,
            UpdatedAt = created,
            DeletedAt = null,
            IsActive = true
        };

        Assert.True(first.Equals(second));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_ForDifferentValues()
    {
        var first = new UserRole(1, 2);
        var second = new UserRole(3, 4);

        Assert.False(first.Equals(second));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_ForDifferentObjectType()
    {
        var userRole = new UserRole();

        Assert.False(userRole.Equals("not a user role"));
    }

    [Fact]
    public void GetHashCode_ShouldMatch_ForSameValues()
    {
        var created = DateTime.UtcNow;

        var first = new UserRole
        {
            Id = 1,
            UserId = 5,
            RoleId = 2,
            CreatedAt = created,
            UpdatedAt = created,
            DeletedAt = null,
            IsActive = true
        };

        var second = new UserRole
        {
            Id = 1,
            UserId = 5,
            RoleId = 2,
            CreatedAt = created,
            UpdatedAt = created,
            DeletedAt = null,
            IsActive = true
        };

        Assert.Equal(first.GetHashCode(), second.GetHashCode());
    }

    [Fact]
    public void NavigationProperties_ShouldBeSettable()
    {
        var user = new User { Id = 1, Email = "test@test.com" };
        var role = new Role { Id = 2, Name = "Admin" };

        var userRole = new UserRole
        {
            User = user,
            Role = role
        };

        Assert.Equal("test@test.com", userRole.User.Email);
        Assert.Equal("Admin", userRole.Role.Name);
    }
}