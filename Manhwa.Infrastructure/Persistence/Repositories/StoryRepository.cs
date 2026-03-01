using Microsoft.EntityFrameworkCore;
using Manhwa.Domain.Entities;
using Manhwa.Domain.Repositories;
using Manhwa.Infrastructure.Persistence;

namespace Manhwa.Infrastructure.Persistence.Repositories;

public class StoryRepository : IStoryRepository
{
    private readonly AppDbContext _context;

    public StoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Story story, CancellationToken ct = default)
    {
        await _context.Stories.AddAsync(story, ct);
    }

    public async Task<Story?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Stories
            .Include(x => x.StoryGenres)
            .FirstOrDefaultAsync(x => x.StoryId == id, ct);
    }

    public void Update(Story story)
    {
        _context.Stories.Update(story);
    }

    public void Remove(Story story)
    {
        _context.Stories.Remove(story);
    }
}