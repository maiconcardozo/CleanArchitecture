using MediatR;
using CleanArchitecture.Application.Templates.DTOs;

namespace CleanArchitecture.Application.Templates.Queries.GetTemplates;

public class GetTemplatesQuery : IRequest<IEnumerable<TemplateDto>>
{
}
