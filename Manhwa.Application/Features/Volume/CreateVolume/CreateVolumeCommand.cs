using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Volume.CreateVolume
{
    public class CreateVolumeCommand : IRequest<CreateVolumeResponse>
    {
        public int StoryId { get; }
        public bool IsAdmin { get; }
        public CreateVolumeRequest Request { get; }

        public CreateVolumeCommand(int storyId, bool isAdmin, CreateVolumeRequest request)
        {
            StoryId = storyId;
            IsAdmin = isAdmin;
            Request = request;
        }
    }
}
