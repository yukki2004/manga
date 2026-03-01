using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.Chapter.CreateChapter
{
    public class CreateChapterRequest
    {
        public int LanguageId { get; set; }
        public float ChapterNumber { get; set; }
        public string Title { get; set; } = null!;
        public List<IFormFile> Pages { get; set; } = new();
    }
}
