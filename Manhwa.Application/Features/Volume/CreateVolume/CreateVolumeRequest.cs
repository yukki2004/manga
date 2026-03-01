using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Volume.CreateVolume
{
    public class CreateVolumeRequest
    {
        public int LanguageId { get; set; }
        public float VolumeNumber { get; set; }
        public string Title { get; set; } = null!;

        public IFormFile Thumbnail { get; set; } = null!;
        public List<IFormFile> Pages { get; set; } = new();
    }
}
