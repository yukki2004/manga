using Manhwa.Application.Common.Interfaces;
using Manhwa.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Volume.DeleteVolume
{
    public class DeleteVolumeCommandHandler : IRequestHandler<DeleteVolumeCommand>
    {
        private readonly IVolumeRepository _volumeRepo;
        private readonly IUnitOfWork _uow;

        public DeleteVolumeCommandHandler(
            IVolumeRepository volumeRepo,
            IUnitOfWork uow)
        {
            _volumeRepo = volumeRepo;
            _uow = uow;
        }

        public async Task Handle(DeleteVolumeCommand cmd, CancellationToken ct)
        {
            if (!cmd.IsAdmin)
                throw new UnauthorizedAccessException("Only admin can delete volume");

            var volume = await _volumeRepo.GetByIdAsync(cmd.VolumeId, ct)
                ?? throw new Exception("Volume not found");

            _volumeRepo.Remove(volume);

            await _uow.SaveChangesAsync(ct);
        }
    }
}
