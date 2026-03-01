using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Story.AddStory
{
    public class CreateStoryResponse
    {
        public int StoryId { get; set; }
        public string Title { get; set; } = null!;
        public string ThumbnailUrl { get; set; } = null!;
    }
}
