using MediatR;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Events;

namespace CleanArchitecture.Application.Templates.Commands.DeleteTemplate;

public class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand, bool>
{
    private readonly ITemplateRepository _templateRepository;

    public DeleteTemplateCommandHandler(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<bool> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (template == null)
        {
            throw new TemplateNotFoundException(request.Id);
        }
        
        template.AddDomainEvent(new TemplateDeletedEvent(template.Id, template.Name));
        
        await _templateRepository.DeleteAsync(template, cancellationToken);
        
        return true;
    }
}
