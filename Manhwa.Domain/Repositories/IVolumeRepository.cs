using Manhwa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Repositories
{
    public interface IVolumeRepository
    {
        Task<Volume?> GetByIdAsync(int id, CancellationToken ct = default);

        Task AddAsync(Volume volume, CancellationToken ct = default);

        void Update(Volume volume);

        void Remove(Volume volume);

        Task<bool> ExistsAsync(
            int storyId,
            int languageId,
            float volumeNumber,
            CancellationToken ct = default);
    }
}
