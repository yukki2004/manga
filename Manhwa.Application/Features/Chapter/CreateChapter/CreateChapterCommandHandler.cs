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

namespace Manhwa.Application.Features.Chapter.CreateChapter
{
    public class CreateChapterCommandHandler
        : IRequestHandler<CreateChapterCommand, CreateChapterResponse>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly ILanguageRepository _languageRepo;
        private readonly IChapterRepository _chapterRepo;
        private readonly IChapterImageRepository _imageRepo;
        private readonly IStorageService _storage;
        private readonly IUnitOfWork _uow;

        public CreateChapterCommandHandler(
            IStoryRepository storyRepo,
            ILanguageRepository languageRepo,
            IChapterRepository chapterRepo,
            IChapterImageRepository imageRepo,
            IStorageService storage,
            IUnitOfWork uow)
        {
            _storyRepo = storyRepo;
            _languageRepo = languageRepo;
            _chapterRepo = chapterRepo;
            _imageRepo = imageRepo;
            _storage = storage;
            _uow = uow;
        }

        public async Task<CreateChapterResponse> Handle(CreateChapterCommand cmd, CancellationToken ct)
        {
            if (!cmd.IsAdmin)
                throw new UnauthorizedAccessException("Only admin can add chapter");

            var req = cmd.Request;

            var story = await _storyRepo.GetByIdAsync(cmd.StoryId, ct);
            if (story == null)
                throw new Exception("Story not found");

            var language = await _languageRepo.GetByIdAsync(req.LanguageId, ct);
            if (language == null)
                throw new Exception("Language not found");

            if (req.Pages == null || req.Pages.Count == 0)
                throw new Exception("At least one page is required");

            var exists = await _chapterRepo.ExistsAsync(
                cmd.StoryId, req.LanguageId, req.ChapterNumber, ct);

            if (exists)
                throw new Exception("Chapter already exists for this language");

            var chapter = new Domain.Entities.Chapter
            {
                StoryId = cmd.StoryId,
                LanguageId = req.LanguageId,
                ChapterNumber = req.ChapterNumber,
                Title = req.Title,
                CreatedAt = DateTime.UtcNow
            };

            await _chapterRepo.AddAsync(chapter, ct);
            await _uow.SaveChangesAsync(ct); 

            var images = new List<ChapterImage>();

            for (int i = 0; i < req.Pages.Count; i++)
            {
                var file = req.Pages[i];
                if (file == null || file.Length == 0) continue;

                var order = i + 1;
                var ext = Path.GetExtension(file.FileName);
                var path = StoragePath.ChapterPage(cmd.StoryId, chapter.ChapterId, order, ext);

                using var stream = file.OpenReadStream();
                var relativePath = await _storage.UploadAsync(
                    stream,
                    path,
                    file.ContentType,
                    true,
                    ct);

                images.Add(new ChapterImage
                {
                    ChapterId = chapter.ChapterId,
                    ImageUrl = relativePath,
                    OrderIndex = order
                });
            }

            await _imageRepo.AddRangeAsync(images, ct);

            await _uow.SaveChangesAsync(ct);

            return new CreateChapterResponse
            {
                ChapterId = chapter.ChapterId,
                StoryId = chapter.StoryId,
                ChapterNumber = chapter.ChapterNumber,
                PageCount = images.Count
            };
        }
    }
}
