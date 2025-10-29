using CleanArchitecture.Domain.Entities.TemplateAggregate;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.UnitTests.Domain.Entities;

public class TemplateTests
{
    [Fact]
    public void Constructor_ShouldCreateTemplateWithDefaultValues()
    {
        // Act
        var template = new Template();

        // Assert
        template.Id.Should().Be(0);
        template.Name.Should().BeEmpty();
        template.Description.Should().BeEmpty();
        template.IsActive.Should().BeTrue();
        template.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        template.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Constructor_WithParameters_ShouldSetNameAndDescription()
    {
        // Arrange
        var name = "Test Template";
        var description = "Test Description";

        // Act
        var template = new Template(name, description);

        // Assert
        template.Name.Should().Be(name);
        template.Description.Should().Be(description);
        template.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Update_ShouldUpdateAllProperties()
    {
        // Arrange
        var template = new Template("Old Name", "Old Description");
        var newName = "New Name";
        var newDescription = "New Description";
        var isActive = false;

        // Act
        template.Update(newName, newDescription, isActive);

        // Assert
        template.Name.Should().Be(newName);
        template.Description.Should().Be(newDescription);
        template.IsActive.Should().Be(isActive);
        template.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Activate_ShouldSetIsActiveToTrue()
    {
        // Arrange
        var template = new Template("Test", "Test");
        template.Deactivate();

        // Act
        template.Activate();

        // Assert
        template.IsActive.Should().BeTrue();
        template.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        // Arrange
        var template = new Template("Test", "Test");

        // Act
        template.Deactivate();

        // Assert
        template.IsActive.Should().BeFalse();
        template.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}
