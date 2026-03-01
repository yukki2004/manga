using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class ChapterImage
    {
        public int ChapterImageId { get; set; }
        public int ChapterId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int OrderIndex { get; set; }
        public Chapter Chapter { get; set; } = null!;
    }
}
