using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities.TemplateAggregate;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Infrastructure.Repositories;

public class TemplateRepository : ITemplateRepository
{
    private readonly ApplicationDbContext _context;

    public TemplateRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Template?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Templates.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Template>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Templates.ToListAsync(cancellationToken);
    }

    public async Task<Template> AddAsync(Template template, CancellationToken cancellationToken = default)
    {
        await _context.Templates.AddAsync(template, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return template;
    }

    public async Task UpdateAsync(Template template, CancellationToken cancellationToken = default)
    {
        _context.Templates.Update(template);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Template template, CancellationToken cancellationToken = default)
    {
        _context.Templates.Remove(template);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
