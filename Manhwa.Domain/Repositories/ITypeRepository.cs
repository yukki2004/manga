using Manhwa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Manhwa.Domain.Entities.Type;

namespace Manhwa.Domain.Repositories
{
    public interface ITypeRepository
    {
        Task<Type?> GetByIdAsync(int id, CancellationToken ct = default);
    }
}
