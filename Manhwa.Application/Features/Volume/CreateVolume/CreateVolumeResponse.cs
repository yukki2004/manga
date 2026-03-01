using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Volume.CreateVolume
{
    public class CreateVolumeResponse
    {
        public int VolumeId { get; set; }
        public int StoryId { get; set; }
        public float VolumeNumber { get; set; }
        public string ThumbnailUrl { get; set; } = null!;
        public int PageCount { get; set; }
    }
}
