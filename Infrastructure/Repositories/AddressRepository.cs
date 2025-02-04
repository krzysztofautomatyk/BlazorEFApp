using BlazorEFApp.Domain.Entities;
using BlazorEFApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlazorEFApp.Infrastructure.Repositories;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Address>> GetAllWithClientTypesAsync()
    {
        return await _context.Addresses
            .AsNoTracking()
            .Include(a => a.ClientType)
            .ToListAsync();
    }
}

