using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Templates.DTOs;
using CleanArchitecture.Domain.Entities.TemplateAggregate;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Events;

namespace CleanArchitecture.Application.Templates.Commands.CreateTemplate;

public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, TemplateDto>
{
    private readonly ITemplateRepository _templateRepository;
    private readonly IMapper _mapper;

    public CreateTemplateCommandHandler(ITemplateRepository templateRepository, IMapper mapper)
    {
        _templateRepository = templateRepository;
        _mapper = mapper;
    }

    public async Task<TemplateDto> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = new Template(request.Name, request.Description);
        
        template.AddDomainEvent(new TemplateCreatedEvent(template.Id, template.Name));
        
        var createdTemplate = await _templateRepository.AddAsync(template, cancellationToken);
        
        return _mapper.Map<TemplateDto>(createdTemplate);
    }
}
