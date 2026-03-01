using Manhwa.Domain.Entities;
using Manhwa.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Infrastructure.Persistence.Repositories
{
    public class VolumeImageRepository : IVolumeImageRepository
    {
        private readonly AppDbContext _context;

        public VolumeImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<VolumeImage> images, CancellationToken ct = default)
        {
            await _context.VolumeImages.AddRangeAsync(images, ct);
        }

        public async Task<List<VolumeImage>> GetByVolumeIdAsync(int volumeId, CancellationToken ct = default)
        {
            return await _context.VolumeImages
                .Where(x => x.VolumeId == volumeId)
                .OrderBy(x => x.OrderIndex)
                .ToListAsync(ct);
        }

        public void RemoveRange(IEnumerable<VolumeImage> images)
        {
            _context.VolumeImages.RemoveRange(images);
        }
    }
}
