using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Templates.DTOs;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Application.Templates.Queries.GetTemplate;

public class GetTemplateQueryHandler : IRequestHandler<GetTemplateQuery, TemplateDto?>
{
    private readonly ITemplateRepository _templateRepository;
    private readonly IMapper _mapper;

    public GetTemplateQueryHandler(ITemplateRepository templateRepository, IMapper mapper)
    {
        _templateRepository = templateRepository;
        _mapper = mapper;
    }

    public async Task<TemplateDto?> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetByIdAsync(request.Id, cancellationToken);
        
        return template == null ? null : _mapper.Map<TemplateDto>(template);
    }
}
