using MediatR;
using CleanArchitecture.Application.Templates.DTOs;

namespace CleanArchitecture.Application.Templates.Queries.GetTemplate;

public class GetTemplateQuery : IRequest<TemplateDto?>
{
    public int Id { get; set; }

    public GetTemplateQuery(int id)
    {
        Id = id;
    }
}
