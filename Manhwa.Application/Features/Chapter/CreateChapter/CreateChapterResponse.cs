using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Chapter.CreateChapter
{
    public class CreateChapterResponse
    {
        public int ChapterId { get; set; }
        public int StoryId { get; set; }
        public float ChapterNumber { get; set; }
        public int PageCount { get; set; }
    }
}
