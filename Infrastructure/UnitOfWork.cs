using BlazorEFApp.Domain.Entities;
using BlazorEFApp.Domain.Repositories;
using BlazorEFApp.Infrastructure.Data;

using System;

namespace BlazorEFApp.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IGenericRepository<Address> Addresses { get; }
    public IGenericRepository<ClientType> ClientTypes { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Addresses = new GenericRepository<Address>(context);
        ClientTypes = new GenericRepository<ClientType>(context);
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task BeginTransactionAsync() =>
        await _context.BeginTransactionAsync();

    public async Task CommitTransactionAsync() =>
        await _context.CommitTransactionAsync();

    public async Task RollbackTransactionAsync() =>
        await _context.RollbackTransactionAsync();

    public void Dispose() => _context.Dispose();
}
