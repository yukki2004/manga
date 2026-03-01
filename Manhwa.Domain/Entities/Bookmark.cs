using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class Bookmark
    {
        public int BookmarkId { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; } = null!;
        public bool IsSystem { get; set; }

        public User? User { get; set; } 
        public ICollection<StoryBookmark> StoryBookmarks { get; set; } = new List<StoryBookmark>();
    }
}
