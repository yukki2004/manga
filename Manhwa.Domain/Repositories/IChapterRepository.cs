using Manhwa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Repositories
{
    public interface IChapterRepository
    {
        Task AddAsync(Chapter chapter, CancellationToken ct = default);
        Task<Chapter?> GetByIdAsync(int id, CancellationToken ct = default);
        void Remove(Chapter chapter);
        Task<bool> ExistsAsync(
            int storyId,
            int languageId,
            float chapterNumber,
            CancellationToken ct = default);
    }
}
