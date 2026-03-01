using Manhwa.Application.Common.Interfaces;
using Manhwa.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Chapter.DeleteChapter
{
    public class DeleteChapterCommandHandler
        : IRequestHandler<DeleteChapterCommand>
    {
        private readonly IChapterRepository _chapterRepo;
        private readonly IUnitOfWork _uow;

        public DeleteChapterCommandHandler(
            IChapterRepository chapterRepo,
            IUnitOfWork uow)
        {
            _chapterRepo = chapterRepo;
            _uow = uow;
        }

        public async Task Handle(DeleteChapterCommand request, CancellationToken ct)
        {
            if (!request.IsAdmin)
                throw new UnauthorizedAccessException("Admin only");

            var chapter = await _chapterRepo.GetByIdAsync(request.ChapterId, ct);

            if (chapter == null || chapter.StoryId != request.StoryId)
                throw new Exception("Chapter not found");

            _chapterRepo.Remove(chapter);

            await _uow.SaveChangesAsync(ct);
        }
    }
}
