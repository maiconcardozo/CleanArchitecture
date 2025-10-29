using CleanArchitecture.Application.Templates.Commands.UpdateTemplate;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.UnitTests.Application.Validators;

public class UpdateTemplateCommandValidatorTests
{
    private readonly UpdateTemplateCommandValidator _validator;

    public UpdateTemplateCommandValidatorTests()
    {
        _validator = new UpdateTemplateCommandValidator();
    }

    [Fact]
    public void Validate_WithValidCommand_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var command = new UpdateTemplateCommand
        {
            Id = 1,
            Name = "Valid Template Name",
            Description = "Valid Description",
            IsActive = true
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithInvalidId_ShouldHaveValidationError()
    {
        // Arrange
        var command = new UpdateTemplateCommand
        {
            Id = 0,
            Name = "Valid Name",
            Description = "Valid Description"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id");
    }

    [Fact]
    public void Validate_WithEmptyName_ShouldHaveValidationError()
    {
        // Arrange
        var command = new UpdateTemplateCommand
        {
            Id = 1,
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
        var command = new UpdateTemplateCommand
        {
            Id = 1,
            Name = new string('a', 101), // 101 characters
            Description = "Valid Description"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }
}
