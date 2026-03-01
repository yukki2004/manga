using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class Volume
    {
        public int VolumeId { get; set; }
        public int StoryId { get; set; }
        public int LanguageId { get; set; }

        public float VolumeNumber { get; set; }
        public string Title { get; set; } = null!;
        public string ThumbnailUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public Story Story { get; set; } = null!;
        public Language Language { get; set; } = null!;

        public ICollection<VolumeImage> Images { get; set; } = new List<VolumeImage>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>();

    }
}
