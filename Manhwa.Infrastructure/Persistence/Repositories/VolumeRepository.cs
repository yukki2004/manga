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
    public class VolumeRepository : IVolumeRepository
    {
        private readonly AppDbContext _context;

        public VolumeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Volume?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Volumes
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.VolumeId == id, ct);
        }

        public async Task AddAsync(Volume volume, CancellationToken ct = default)
        {
            await _context.Volumes.AddAsync(volume, ct);
        }

        public void Update(Volume volume)
        {
            _context.Volumes.Update(volume);
        }

        public void Remove(Volume volume)
        {
            _context.Volumes.Remove(volume);
        }

        public async Task<bool> ExistsAsync(
            int storyId,
            int languageId,
            float volumeNumber,
            CancellationToken ct = default)
        {
            return await _context.Volumes.AnyAsync(x =>
                x.StoryId == storyId &&
                x.LanguageId == languageId &&
                x.VolumeNumber == volumeNumber,
                ct);
        }
    }
}
