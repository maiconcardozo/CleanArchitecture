using MediatR;

namespace CleanArchitecture.Application.Templates.Commands.DeleteTemplate;

public class DeleteTemplateCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeleteTemplateCommand(int id)
    {
        Id = id;
    }
}
