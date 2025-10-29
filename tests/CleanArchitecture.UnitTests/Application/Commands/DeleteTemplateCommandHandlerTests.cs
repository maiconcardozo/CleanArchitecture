using AutoMapper;
using CleanArchitecture.Application.Templates.Commands.DeleteTemplate;
using CleanArchitecture.Domain.Entities.TemplateAggregate;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanArchitecture.UnitTests.Application.Commands;

public class DeleteTemplateCommandHandlerTests
{
    private readonly Mock<ITemplateRepository> _mockRepository;
    private readonly DeleteTemplateCommandHandler _handler;

    public DeleteTemplateCommandHandlerTests()
    {
        _mockRepository = new Mock<ITemplateRepository>();
        _handler = new DeleteTemplateCommandHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_WithExistingTemplate_ShouldDeleteTemplate()
    {
        // Arrange
        var templateId = 1;
        var template = new Template("Test", "Test") { Id = templateId };
        var command = new DeleteTemplateCommand(templateId);

        _mockRepository
            .Setup(r => r.GetByIdAsync(templateId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(template);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        _mockRepository.Verify(r => r.DeleteAsync(template, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingTemplate_ShouldThrowNotFoundException()
    {
        // Arrange
        var templateId = 999;
        var command = new DeleteTemplateCommand(templateId);

        _mockRepository
            .Setup(r => r.GetByIdAsync(templateId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Template?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<TemplateNotFoundException>();
        _mockRepository.Verify(r => r.DeleteAsync(It.IsAny<Template>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
