using Manhwa.Domain.Entities;
using Manhwa.Domain.Repositories;

namespace Manhwa.Infrastructure.Persistence.Repositories;

public class ChapterImageRepository : IChapterImageRepository
{
    private readonly AppDbContext _context;

    public ChapterImageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<ChapterImage> images, CancellationToken ct = default)
    {
        await _context.ChapterImages.AddRangeAsync(images, ct);
    }
}