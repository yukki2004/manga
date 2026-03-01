using Microsoft.EntityFrameworkCore;
using Manhwa.Domain.Repositories;
using Manhwa.Infrastructure.Persistence;
using Manhwa.Domain.Entities;
using Type = Manhwa.Domain.Entities.Type;

namespace Manhwa.Infrastructure.Persistence.Repositories;

public class TypeRepository : ITypeRepository
{
    private readonly AppDbContext _context;

    public TypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Type?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Types
            .FirstOrDefaultAsync(x => x.TypeId == id, ct);
    }
}