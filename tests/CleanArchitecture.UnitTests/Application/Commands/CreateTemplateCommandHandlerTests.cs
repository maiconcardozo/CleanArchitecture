using AutoMapper;
using CleanArchitecture.Application.Templates.Commands.CreateTemplate;
using CleanArchitecture.Application.Templates.DTOs;
using CleanArchitecture.Domain.Entities.TemplateAggregate;
using CleanArchitecture.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanArchitecture.UnitTests.Application.Commands;

public class CreateTemplateCommandHandlerTests
{
    private readonly Mock<ITemplateRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreateTemplateCommandHandler _handler;

    public CreateTemplateCommandHandlerTests()
    {
        _mockRepository = new Mock<ITemplateRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new CreateTemplateCommandHandler(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateTemplate()
    {
        // Arrange
        var command = new CreateTemplateCommand
        {
            Name = "Test Template",
            Description = "Test Description"
        };

        var template = new Template(command.Name, command.Description);
        var templateDto = new TemplateDto
        {
            Id = 1,
            Name = command.Name,
            Description = command.Description,
            IsActive = true
        };

        _mockRepository
            .Setup(r => r.AddAsync(It.IsAny<Template>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(template);

        _mockMapper
            .Setup(m => m.Map<TemplateDto>(It.IsAny<Template>()))
            .Returns(templateDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.Description.Should().Be(command.Description);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Template>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
