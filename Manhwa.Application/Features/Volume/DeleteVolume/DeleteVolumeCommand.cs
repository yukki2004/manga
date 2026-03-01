using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Volume.DeleteVolume
{
    public class DeleteVolumeCommand : IRequest
    {
        public int VolumeId { get; }
        public bool IsAdmin { get; }

        public DeleteVolumeCommand(int volumeId, bool isAdmin)
        {
            VolumeId = volumeId;
            IsAdmin = isAdmin;
        }
    }
}
