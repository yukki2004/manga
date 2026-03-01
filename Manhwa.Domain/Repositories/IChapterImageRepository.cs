using Manhwa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Repositories
{
    public interface IChapterImageRepository
    {
        Task AddRangeAsync(IEnumerable<ChapterImage> images, CancellationToken ct = default);
    }
}
