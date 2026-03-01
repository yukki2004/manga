using Manhwa.Domain.Entities;
using Manhwa.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Manhwa.Infrastructure.Persistence.Repositories;

public class ChapterRepository : IChapterRepository
{
    private readonly AppDbContext _context;

    public ChapterRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Chapter chapter, CancellationToken ct = default)
    {
        await _context.Chapters.AddAsync(chapter, ct);
    }

    public async Task<Chapter?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Chapters
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.ChapterId == id, ct);
    }

    public async Task<bool> ExistsAsync(
        int storyId,
        int languageId,
        float chapterNumber,
        CancellationToken ct = default)
    {
        return await _context.Chapters.AnyAsync(
            x => x.StoryId == storyId
              && x.LanguageId == languageId
              && x.ChapterNumber == chapterNumber,
            ct);
    }
    public void Remove(Chapter chapter)
    => _context.Chapters.Remove(chapter);
}