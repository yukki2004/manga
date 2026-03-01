using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Entities
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string Code { get; set; } = null!;  
        public string Name { get; set; } = null!;  

        public ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
        public ICollection<Volume> Volumes { get; set; } = new List<Volume>();
        public ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>();

    }
}
