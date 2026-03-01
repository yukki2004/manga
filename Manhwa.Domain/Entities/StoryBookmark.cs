using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class StoryBookmark
    {

        public int UserId { get; set; }
        public int StoryId { get; set; }
        public int BookmarkId { get; set; }

        public DateTime UpdatedAt { get; set; }

        public User User { get; set; } = null!;
        public Story Story { get; set; } = null!;
        public Bookmark Bookmark { get; set; } = null!;
    }
}
