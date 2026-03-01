using Manhwa.Application.Common.Interfaces;
using Manhwa.Application.Common.Storage;
using Manhwa.Domain.Entities;
using Manhwa.Domain.Enums;
using Manhwa.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Story.AddStory
{
    public class CreateStoryCommandHandler
        : IRequestHandler<CreateStoryCommand, CreateStoryResponse>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly ITypeRepository _typeRepo;
        private readonly IStoryGenreRepository _storyGenreRepo;
        private readonly IStorageService _storage;
        private readonly IUnitOfWork _uow;

        public CreateStoryCommandHandler(
            IStoryRepository storyRepo,
            IGenreRepository genreRepo,
            ITypeRepository typeRepo,
            IStoryGenreRepository storyGenreRepo,
            IStorageService storage,
            IUnitOfWork uow)
        {
            _storyRepo = storyRepo;
            _genreRepo = genreRepo;
            _typeRepo = typeRepo;
            _storyGenreRepo = storyGenreRepo;
            _storage = storage;
            _uow = uow;
        }

        public async Task<CreateStoryResponse> Handle(CreateStoryCommand cmd, CancellationToken ct)
        {
            var req = cmd.Request;

            if (!cmd.IsAdmin)
                throw new UnauthorizedAccessException("Only admin can create story");

            var type = await _typeRepo.GetByIdAsync(req.TypeId, ct);
            if (type == null)
                throw new Exception("Type not found");

            var genres = await _genreRepo.GetByIdsAsync(req.GenreIds, ct);
            if (genres.Count != req.GenreIds.Count)
                throw new Exception("Some genres not found");

            var story = new Domain.Entities.Story
            {
                Title = req.Title,
                OtherTitle = req.OtherTitle,
                Description = req.Description,
                Author = req.Author,
                ReleaseYear = req.ReleaseYear,
                TypeId = req.TypeId,
                UserId = cmd.CurrentUserId,
                Status = req.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ThumbnailUrl = "" 
            };

            await _storyRepo.AddAsync(story, ct);
            await _uow.SaveChangesAsync(ct); 

            var ext = Path.GetExtension(req.Thumbnail.FileName);
            var path = StoragePath.StoryThumbnail(story.StoryId, ext);

            using var stream = req.Thumbnail.OpenReadStream();
            var relativePath = await _storage.UploadAsync(
                stream,
                path,
                req.Thumbnail.ContentType,
                true,
                ct);

            story.ThumbnailUrl = relativePath;
            _storyRepo.Update(story);

            var storyGenres = genres.Select(g => new StoryGenre
            {
                StoryId = story.StoryId,
                GenreId = g.GenreId
            });

            await _storyGenreRepo.AddRangeAsync(storyGenres, ct);

            await _uow.SaveChangesAsync(ct);

            return new CreateStoryResponse
            {
                StoryId = story.StoryId,
                Title = story.Title,
                ThumbnailUrl = story.ThumbnailUrl
            };
        }
    }
}
