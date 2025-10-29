namespace CleanArchitecture.Domain.Exceptions;

public class TemplateNotFoundException : Exception
{
    public TemplateNotFoundException(int templateId)
        : base($"Template with ID {templateId} was not found.")
    {
    }
}
