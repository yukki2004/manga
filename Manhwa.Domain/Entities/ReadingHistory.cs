using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class ReadingHistory
    {
        public int UserId { get; set; }
        public int StoryId { get; set; }

        public int? ChapterId { get; set; }
        public int? VolumeId { get; set; }
        public int LanguageId { get; set; }

        public DateTime UpdatedAt { get; set; }

        public User User { get; set; } = null!;
        public Story Story { get; set; } = null!;
        public Chapter? Chapter { get; set; }
        public Volume? Volume { get; set; }
        public Language Language { get; set; } = null!;
    }
}
