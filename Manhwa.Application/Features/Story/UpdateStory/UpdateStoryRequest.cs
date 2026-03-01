using Manhwa.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Story.UpdateStory
{
    public class UpdateStoryRequest
    {

        public string Title { get; set; } = null!;
        public string OtherTitle { get; set; } = null!;
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int ReleaseYear { get; set; }

        public int TypeId { get; set; }
        public StoryStatus Status { get; set; }

        public IFormFile? Thumbnail { get; set; } // optional
    }
}
