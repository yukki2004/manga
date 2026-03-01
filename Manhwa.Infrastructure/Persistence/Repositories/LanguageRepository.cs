using Manhwa.Domain.Entities;
using Manhwa.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Manhwa.Infrastructure.Persistence.Repositories;

public class LanguageRepository : ILanguageRepository
{
    private readonly AppDbContext _context;

    public LanguageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Language?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Languages
            .FirstOrDefaultAsync(x => x.LanguageId == id, ct);
    }

    public async Task<List<Language>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Languages
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
    }
}