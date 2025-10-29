using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Events;

public class TemplateCreatedEvent : DomainEvent
{
    public int TemplateId { get; }
    public string Name { get; }

    public TemplateCreatedEvent(int templateId, string name)
    {
        TemplateId = templateId;
        Name = name;
    }
}
