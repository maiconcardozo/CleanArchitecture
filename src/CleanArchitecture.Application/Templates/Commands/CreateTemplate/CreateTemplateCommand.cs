using MediatR;
using CleanArchitecture.Application.Templates.DTOs;

namespace CleanArchitecture.Application.Templates.Commands.CreateTemplate;

public class CreateTemplateCommand : IRequest<TemplateDto>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
