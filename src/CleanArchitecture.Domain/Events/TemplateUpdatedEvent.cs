using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Events;

public class TemplateUpdatedEvent : DomainEvent
{
    public int TemplateId { get; }
    public string Name { get; }

    public TemplateUpdatedEvent(int templateId, string name)
    {
        TemplateId = templateId;
        Name = name;
    }
}
