using BlazorEFApp.Domain.Entities;
using BlazorEFApp.Domain.Repositories;
using BlazorEFApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlazorEFApp.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
    private AppDbContext? _context;

    public IGenericRepository<Address> Addresses { get; }
    public IGenericRepository<ClientType> ClientTypes { get; }

    public UnitOfWork(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        // Tworzymy DbContext dopiero przy pierwszym użyciu
        _context = _dbContextFactory.CreateDbContext();
        Addresses = new GenericRepository<Address>(_context);
        ClientTypes = new GenericRepository<ClientType>(_context);
    }

    public async Task<int> SaveChangesAsync() => await _context!.SaveChangesAsync();

    public async Task BeginTransactionAsync() =>
        await _context.BeginTransactionAsync();

    public async Task CommitTransactionAsync() =>
        await _context.CommitTransactionAsync();

    public async Task RollbackTransactionAsync() =>
        await _context.RollbackTransactionAsync();

    public void Dispose() => _context?.Dispose();
}
