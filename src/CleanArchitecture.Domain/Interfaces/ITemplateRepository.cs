using CleanArchitecture.Domain.Entities.TemplateAggregate;

namespace CleanArchitecture.Domain.Interfaces;

public interface ITemplateRepository
{
    Task<Template?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Template>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Template> AddAsync(Template template, CancellationToken cancellationToken = default);
    Task UpdateAsync(Template template, CancellationToken cancellationToken = default);
    Task DeleteAsync(Template template, CancellationToken cancellationToken = default);
}
