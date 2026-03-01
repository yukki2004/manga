using Manhwa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Repositories
{
    public interface IVolumeImageRepository
    {
        Task AddRangeAsync(IEnumerable<VolumeImage> images, CancellationToken ct = default);

        Task<List<VolumeImage>> GetByVolumeIdAsync(int volumeId, CancellationToken ct = default);

        void RemoveRange(IEnumerable<VolumeImage> images);
    }
}
