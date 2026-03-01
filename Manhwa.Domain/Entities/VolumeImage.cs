using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class VolumeImage
    {
        public int VolumeImageId { get; set; }
        public int VolumeId { get; set; }

        public string ImageUrl { get; set; } = null!;
        public int OrderIndex { get; set; }

        public Volume Volume { get; set; } = null!;
    }
}
