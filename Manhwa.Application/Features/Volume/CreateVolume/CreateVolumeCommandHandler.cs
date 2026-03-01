using Manhwa.Application.Common.Interfaces;
using Manhwa.Application.Common.Storage;
using Manhwa.Domain.Entities;
using Manhwa.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Volume.CreateVolume
{
    public class CreateVolumeCommandHandler
        : IRequestHandler<CreateVolumeCommand, CreateVolumeResponse>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly ILanguageRepository _languageRepo;
        private readonly IVolumeRepository _volumeRepo;
        private readonly IVolumeImageRepository _imageRepo;
        private readonly IStorageService _storage;
        private readonly IUnitOfWork _uow;

        public CreateVolumeCommandHandler(
            IStoryRepository storyRepo,
            ILanguageRepository languageRepo,
            IVolumeRepository volumeRepo,
            IVolumeImageRepository imageRepo,
            IStorageService storage,
            IUnitOfWork uow)
        {
            _storyRepo = storyRepo;
            _languageRepo = languageRepo;
            _volumeRepo = volumeRepo;
            _imageRepo = imageRepo;
            _storage = storage;
            _uow = uow;
        }

        public async Task<CreateVolumeResponse> Handle(CreateVolumeCommand cmd, CancellationToken ct)
        {
            if (!cmd.IsAdmin)
                throw new UnauthorizedAccessException("Only admin can add volume");

            var req = cmd.Request;

            var story = await _storyRepo.GetByIdAsync(cmd.StoryId, ct)
                ?? throw new Exception("Story not found");

            var language = await _languageRepo.GetByIdAsync(req.LanguageId, ct)
                ?? throw new Exception("Language not found");

            if (req.Thumbnail == null || req.Thumbnail.Length == 0)
                throw new Exception("Thumbnail is required");

            if (req.Pages == null || req.Pages.Count == 0)
                throw new Exception("At least one page is required");

            var exists = await _volumeRepo.ExistsAsync(
                cmd.StoryId,
                req.LanguageId,
                req.VolumeNumber,
                ct);

            if (exists)
                throw new Exception("Volume already exists for this language");

            var volume = new Domain.Entities.Volume
            {
                StoryId = cmd.StoryId,
                LanguageId = req.LanguageId,
                VolumeNumber = req.VolumeNumber,
                Title = req.Title,
                CreatedAt = DateTime.UtcNow,
                ThumbnailUrl = "temp"
            };

            await _volumeRepo.AddAsync(volume, ct);
            await _uow.SaveChangesAsync(ct);

            // ===== Upload thumbnail =====
            var thumbExt = Path.GetExtension(req.Thumbnail.FileName);

            var thumbPath = StoragePath.VolumeThumbnail(
                cmd.StoryId,
                volume.VolumeId,
                thumbExt);

            using (var stream = req.Thumbnail.OpenReadStream())
            {
                volume.ThumbnailUrl = await _storage.UploadAsync(
                    stream,
                    thumbPath,
                    req.Thumbnail.ContentType,
                    true,
                    ct);
            }

            // ===== Sort pages =====
            var sortedPages = req.Pages
                .Where(x => x != null && x.Length > 0)
                .OrderBy(x =>
                {
                    var name = Path.GetFileNameWithoutExtension(x.FileName);
                    return int.TryParse(name, out var n) ? n : int.MaxValue;
                })
                .ToList();

            var images = new List<VolumeImage>();

            for (int i = 0; i < sortedPages.Count; i++)
            {
                var file = sortedPages[i];
                var order = i + 1;
                var ext = Path.GetExtension(file.FileName);

                var path = StoragePath.VolumePage(
                    cmd.StoryId,
                    volume.VolumeId,
                    order,
                    ext);

                using var stream = file.OpenReadStream();

                var relativePath = await _storage.UploadAsync(
                    stream,
                    path,
                    file.ContentType,
                    true,
                    ct);

                images.Add(new VolumeImage
                {
                    VolumeId = volume.VolumeId,
                    ImageUrl = relativePath,
                    OrderIndex = order
                });
            }

            await _imageRepo.AddRangeAsync(images, ct);
            await _uow.SaveChangesAsync(ct);

            return new CreateVolumeResponse
            {
                VolumeId = volume.VolumeId,
                StoryId = volume.StoryId,
                VolumeNumber = volume.VolumeNumber,
                ThumbnailUrl = volume.ThumbnailUrl,
                PageCount = images.Count
            };
        }
    }
}
