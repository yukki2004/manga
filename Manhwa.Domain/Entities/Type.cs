using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class Type
    {
        public int TypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;

        public ICollection<Story> Stories { get; set; } = new List<Story>();
    }
}
