using Microsoft.EntityFrameworkCore;
using Manhwa.Domain.Entities;
using Manhwa.Domain.Repositories;
using Manhwa.Infrastructure.Persistence;

namespace Manhwa.Infrastructure.Persistence.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Genre>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
    {
        return await _context.Genres
            .Where(x => ids.Contains(x.GenreId))
            .ToListAsync(ct);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken ct = default)
    {
        return await _context.Genres.AnyAsync(x => x.GenreId == id, ct);
    }
}