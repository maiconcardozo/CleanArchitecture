using AutoMapper;
using CleanArchitecture.Application.Templates.DTOs;
using CleanArchitecture.Application.Templates.Queries.GetTemplate;
using CleanArchitecture.Domain.Entities.TemplateAggregate;
using CleanArchitecture.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanArchitecture.UnitTests.Application.Queries;

public class GetTemplateQueryHandlerTests
{
    private readonly Mock<ITemplateRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetTemplateQueryHandler _handler;

    public GetTemplateQueryHandlerTests()
    {
        _mockRepository = new Mock<ITemplateRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetTemplateQueryHandler(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_WithExistingTemplate_ShouldReturnTemplateDto()
    {
        // Arrange
        var templateId = 1;
        var template = new Template("Test", "Test") { Id = templateId };
        var templateDto = new TemplateDto
        {
            Id = templateId,
            Name = "Test",
            Description = "Test",
            IsActive = true
        };

        var query = new GetTemplateQuery(templateId);

        _mockRepository
            .Setup(r => r.GetByIdAsync(templateId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(template);

        _mockMapper
            .Setup(m => m.Map<TemplateDto>(template))
            .Returns(templateDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(templateId);
        result.Name.Should().Be("Test");
    }

    [Fact]
    public async Task Handle_WithNonExistingTemplate_ShouldReturnNull()
    {
        // Arrange
        var templateId = 999;
        var query = new GetTemplateQuery(templateId);

        _mockRepository
            .Setup(r => r.GetByIdAsync(templateId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Template?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}
