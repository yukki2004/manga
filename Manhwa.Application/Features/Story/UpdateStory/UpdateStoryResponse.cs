using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Story.UpdateStory
{
    public class UpdateStoryResponse
    {
        public int StoryId { get; set; }
        public string Title { get; set; } = null!;
        public string ThumbnailUrl { get; set; } = null!;
    }
}
