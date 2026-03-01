using Manhwa.Application.Common.Interfaces;
using Manhwa.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Story.DeleteStory
{
    public class DeleteStoryCommandHandler : IRequestHandler<DeleteStoryCommand>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly IStorageService _storage;
        private readonly IUnitOfWork _uow;

        public DeleteStoryCommandHandler(
            IStoryRepository storyRepo,
            IStorageService storage,
            IUnitOfWork uow)
        {
            _storyRepo = storyRepo;
            _storage = storage;
            _uow = uow;
        }

        public async Task Handle(DeleteStoryCommand cmd, CancellationToken ct)
        {
            if (!cmd.IsAdmin)
                throw new UnauthorizedAccessException("Only admin can delete story");

            var story = await _storyRepo.GetByIdAsync(cmd.StoryId, ct);
            if (story == null)
                throw new Exception("Story not found");

            _storyRepo.Remove(story);
            await _uow.SaveChangesAsync(ct);

            await _storage.DeleteDirectoryAsync($"stories/{cmd.StoryId}", ct);
        }
    }
}
