using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Templates.DTOs;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Application.Templates.Queries.GetTemplates;

public class GetTemplatesQueryHandler : IRequestHandler<GetTemplatesQuery, IEnumerable<TemplateDto>>
{
    private readonly ITemplateRepository _templateRepository;
    private readonly IMapper _mapper;

    public GetTemplatesQueryHandler(ITemplateRepository templateRepository, IMapper mapper)
    {
        _templateRepository = templateRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TemplateDto>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
    {
        var templates = await _templateRepository.GetAllAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<TemplateDto>>(templates);
    }
}
