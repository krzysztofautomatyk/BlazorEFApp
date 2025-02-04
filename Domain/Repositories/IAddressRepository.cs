using BlazorEFApp.Domain.Entities;

namespace BlazorEFApp.Domain.Repositories;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<IEnumerable<Address>> GetAllWithClientTypesAsync();
}

