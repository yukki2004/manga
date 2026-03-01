using Manhwa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Repositories
{
    public interface IStoryGenreRepository
    {
        Task AddRangeAsync(IEnumerable<StoryGenre> items, CancellationToken ct = default);
        Task<List<StoryGenre>> GetByStoryIdAsync(int storyId, CancellationToken ct = default);
        void RemoveRange(IEnumerable<StoryGenre> items);
    }
}
