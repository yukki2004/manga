using Manhwa.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class Story
    {
        public int StoryId { get; set; }
        public string Title { get; set; } = null!;
        public int TypeId { get; set; }   
        public int? UserId { get; set; }
        public string OtherTitle { get; set; } = null!;
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int ReleaseYear { get; set; }
        public string ThumbnailUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public StoryStatus Status { get; set; }

        public ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
        public ICollection<StoryRating> Ratings { get; set; } = new List<StoryRating>();
        public ICollection<Volume> Volumes { get; set; } = new List<Volume>();
        public ICollection<StoryBookmark> StoryBookmarks { get; set; } = new List<StoryBookmark>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<StoryGenre> StoryGenres { get; set; } = new List<StoryGenre>();
        public ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>();
        public Type Type { get; set; } = null!;
        public User? User { get; set; } 


    }
}
