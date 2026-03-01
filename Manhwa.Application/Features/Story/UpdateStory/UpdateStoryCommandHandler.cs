using Manhwa.Application.Common.Interfaces;
using Manhwa.Application.Common.Storage;
using Manhwa.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Story.UpdateStory
{
    public class UpdateStoryCommandHandler
        : IRequestHandler<UpdateStoryCommand, UpdateStoryResponse>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly ITypeRepository _typeRepo;
        private readonly IStorageService _storage;
        private readonly IUnitOfWork _uow;

        public UpdateStoryCommandHandler(
            IStoryRepository storyRepo,
            ITypeRepository typeRepo,
            IStorageService storage,
            IUnitOfWork uow)
        {
            _storyRepo = storyRepo;
            _typeRepo = typeRepo;
            _storage = storage;
            _uow = uow;
        }

        public async Task<UpdateStoryResponse> Handle(UpdateStoryCommand cmd, CancellationToken ct)
        {
            if (!cmd.IsAdmin)
                throw new UnauthorizedAccessException("Only admin can update story");

            var story = await _storyRepo.GetByIdAsync(cmd.StoryId, ct);
            if (story == null)
                throw new Exception("Story not found");

            var req = cmd.Request;

            var type = await _typeRepo.GetByIdAsync(req.TypeId, ct);
            if (type == null)
                throw new Exception("Type not found");

            story.Title = req.Title;
            story.OtherTitle = req.OtherTitle;
            story.Description = req.Description;
            story.Author = req.Author;
            story.ReleaseYear = req.ReleaseYear;
            story.TypeId = req.TypeId;
            story.Status = req.Status;
            story.UpdatedAt = DateTime.UtcNow;

            if (req.Thumbnail != null && req.Thumbnail.Length > 0)
            {
                if (!string.IsNullOrEmpty(story.ThumbnailUrl))
                    await _storage.DeleteAsync(story.ThumbnailUrl, ct);

                var ext = Path.GetExtension(req.Thumbnail.FileName);
                var path = StoragePath.StoryThumbnail(story.StoryId, ext);

                using var stream = req.Thumbnail.OpenReadStream();
                story.ThumbnailUrl = await _storage.UploadAsync(
                    stream, path, req.Thumbnail.ContentType, true, ct);
            }

            _storyRepo.Update(story);
            await _uow.SaveChangesAsync(ct);

            return new UpdateStoryResponse
            {
                StoryId = story.StoryId,
                Title = story.Title,
                ThumbnailUrl = story.ThumbnailUrl
            };
        }
    }
}
