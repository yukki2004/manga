using Manhwa.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; }
        public ICollection<Story> Stories { get; set; } = new List<Story>();
        public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
        public ICollection<StoryRating> StoryRatings { get; set; } = new List<StoryRating>();
        public ICollection<StoryBookmark> StoryBookmarks { get; set; } = new List<StoryBookmark>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>();
    }
}
