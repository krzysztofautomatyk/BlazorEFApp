using Microsoft.EntityFrameworkCore;
using BlazorEFApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlazorEFApp.Infrastructure;

public class AppDbContext : DbContext
{
    private IDbContextTransaction? _currentTransaction;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<ClientType> ClientTypes { get; set; }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            return;
        }
        _currentTransaction = await Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("Cannot commit a transaction that has not been started.");
            }
            await _currentTransaction.CommitAsync();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("Cannot rollback a transaction that has not been started.");
            }
            await _currentTransaction.RollbackAsync();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>().ToTable("Addresses");
        modelBuilder.Entity<ClientType>().ToTable("ClientTypes");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.Street)
                .IsRequired()
                .HasMaxLength(200); // Database constraint

            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PostalCode)
                .HasMaxLength(10);

            entity.HasOne(a => a.ClientType)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.ClientTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ClientType>(entity =>
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50); // Database constraint

            entity.HasIndex(e => e.Name).IsUnique(); // Index
        });

        base.OnModelCreating(modelBuilder);
    }
}


//dotnet ef migrations add InitialCreate
//dotnet ef database update
