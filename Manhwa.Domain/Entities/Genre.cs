using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;

        public ICollection<StoryGenre> StoryGenres { get; set; } = new List<StoryGenre>();
    }
}
