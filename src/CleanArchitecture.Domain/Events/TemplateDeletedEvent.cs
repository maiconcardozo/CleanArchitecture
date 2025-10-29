using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Events;

public class TemplateDeletedEvent : DomainEvent
{
    public int TemplateId { get; }
    public string Name { get; }

    public TemplateDeletedEvent(int templateId, string name)
    {
        TemplateId = templateId;
        Name = name;
    }
}
