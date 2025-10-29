using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Templates.DTOs;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Events;

namespace CleanArchitecture.Application.Templates.Commands.UpdateTemplate;

public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand, TemplateDto>
{
    private readonly ITemplateRepository _templateRepository;
    private readonly IMapper _mapper;

    public UpdateTemplateCommandHandler(ITemplateRepository templateRepository, IMapper mapper)
    {
        _templateRepository = templateRepository;
        _mapper = mapper;
    }

    public async Task<TemplateDto> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (template == null)
        {
            throw new TemplateNotFoundException(request.Id);
        }
        
        template.Update(request.Name, request.Description, request.IsActive);
        template.AddDomainEvent(new TemplateUpdatedEvent(template.Id, template.Name));
        
        await _templateRepository.UpdateAsync(template, cancellationToken);
        
        return _mapper.Map<TemplateDto>(template);
    }
}
