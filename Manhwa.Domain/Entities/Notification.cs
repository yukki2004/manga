using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int StoryId { get; set; }
        public int? ChapterId { get; set; }
        public int? VolumeId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
        public Story Story { get; set; } = null!;
        public Chapter? Chapter { get; set; }
        public Volume? Volume { get; set; }

    }
}
