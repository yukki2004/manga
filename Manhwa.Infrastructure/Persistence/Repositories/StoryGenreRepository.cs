using Microsoft.EntityFrameworkCore;
using Manhwa.Domain.Entities;
using Manhwa.Domain.Repositories;
using Manhwa.Infrastructure.Persistence;

namespace Manhwa.Infrastructure.Persistence.Repositories;

public class StoryGenreRepository : IStoryGenreRepository
{
    private readonly AppDbContext _context;

    public StoryGenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<StoryGenre> items, CancellationToken ct = default)
    {
        await _context.StoryGenres.AddRangeAsync(items, ct);
    }

    public async Task<List<StoryGenre>> GetByStoryIdAsync(int storyId, CancellationToken ct = default)
    {
        return await _context.StoryGenres
            .Where(x => x.StoryId == storyId)
            .ToListAsync(ct);
    }

    public void RemoveRange(IEnumerable<StoryGenre> items)
    {
        _context.StoryGenres.RemoveRange(items);
    }
}