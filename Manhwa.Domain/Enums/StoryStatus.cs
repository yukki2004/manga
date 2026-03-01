using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Enums
{
    public enum StoryStatus : short
    {
        NotYetPublished = 1,   // chưa phát hành
        Releasing = 2,         // đang ra
        Completed = 3,         // hoàn thành
        OnHiatus = 4,          // tạm ngưng
        Discontinued = 5       // drop
    }
}
