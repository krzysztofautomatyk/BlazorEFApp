using BlazorEFApp.Domain.Entities;

namespace BlazorEFApp.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Address> Addresses { get; }
    IGenericRepository<ClientType> ClientTypes { get; }
    Task<int> SaveChangesAsync();

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

}
