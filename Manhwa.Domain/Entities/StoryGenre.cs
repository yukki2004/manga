using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class StoryGenre
    {
        public int StoryId { get; set; }
        public int GenreId { get; set; }

        public Story Story { get; set; } = null!;
        public Genre Genre { get; set; } = null!;
    }
}
