using Manhwa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Domain.Repositories
{
    public interface ILanguageRepository
    {
        Task<Language?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<List<Language>> GetAllAsync(CancellationToken ct = default);
    }
}
