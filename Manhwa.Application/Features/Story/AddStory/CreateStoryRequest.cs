using Manhwa.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Story.AddStory
{
    public class CreateStoryRequest
    {
        public string Title { get; set; } = null!;
        public string OtherTitle { get; set; } = null!;
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int ReleaseYear { get; set; }
        public StoryStatus Status { get; set; }   // ⭐ thêm dòng này

        public int TypeId { get; set; }
        public List<int> GenreIds { get; set; } = new();

        public IFormFile Thumbnail { get; set; } = null!;
    }
}
