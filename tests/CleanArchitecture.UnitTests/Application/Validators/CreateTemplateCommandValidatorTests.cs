using CleanArchitecture.Application.Templates.Commands.CreateTemplate;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.UnitTests.Application.Validators;

public class CreateTemplateCommandValidatorTests
{
    private readonly CreateTemplateCommandValidator _validator;

    public CreateTemplateCommandValidatorTests()
    {
        _validator = new CreateTemplateCommandValidator();
    }

    [Fact]
    public void Validate_WithValidCommand_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var command = new CreateTemplateCommand
        {
            Name = "Valid Template Name",
            Description = "Valid Description"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithEmptyName_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateTemplateCommand
        {
            Name = "",
            Description = "Valid Description"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Fact]
    public void Validate_WithNameTooLong_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateTemplateCommand
        {
            Name = new string('a', 101), // 101 characters
            Description = "Valid Description"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Fact]
    public void Validate_WithDescriptionTooLong_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateTemplateCommand
        {
            Name = "Valid Name",
            Description = new string('a', 501) // 501 characters
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Description");
    }
}
