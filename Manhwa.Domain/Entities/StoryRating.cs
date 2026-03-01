using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class StoryRating
    {
        public int UserId { get; set; }
        public int StoryId { get; set; }

        public decimal Score { get; set; } 
        public User User { get; set; } = null!;
        public Story Story { get; set; } = null!;
    }
}
