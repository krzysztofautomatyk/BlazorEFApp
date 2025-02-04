using BlazorEFApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorEFApp.Infrastructure.Data;

public static class DatabaseSeeder
{
    public static async Task SeedDataIfEmpty(IDbContextFactory<AppDbContext> factory, bool shouldInitialize)
    {
        if (!shouldInitialize)
        {
            return;
        }

        using var context = await factory.CreateDbContextAsync();

        await context.Database.MigrateAsync();

        await InitializeClientTypes(context);
        await InitializeAddresses(context);

        await context.SaveChangesAsync();
    }


    private static async Task InitializeClientTypes(AppDbContext context)
    {
        if (!await context.ClientTypes.AnyAsync())
        {
            var clientTypes = new List<ClientType>
            {
                new ClientType { Name = "Residential" },
                new ClientType { Name = "Commercial" },
                new ClientType { Name = "Industrial" },
                new ClientType { Name = "Government" },
                new ClientType { Name = "Healthcare" }
            };

            await context.ClientTypes.AddRangeAsync(clientTypes);
            await context.SaveChangesAsync();
        }
    }

    private static async Task InitializeAddresses(AppDbContext context)
    {
        if (!await context.Addresses.AnyAsync())
        {
            var clientTypes = await context.ClientTypes.ToListAsync();

            var addresses = new List<Address>
            {
                new Address { Street = "9528 Ave", City = "New York", PostalCode = "13-606", ClientTypeId = clientTypes[0].Id },
                new Address { Street = "4865 St", City = "Los Angeles", PostalCode = "22-374", ClientTypeId = clientTypes[1].Id },
                new Address { Street = "2798 Lane", City = "Chicago", PostalCode = "33-517", ClientTypeId = clientTypes[2].Id },
                new Address { Street = "8865 Lane", City = "Houston", PostalCode = "96-978", ClientTypeId = clientTypes[3].Id },
                new Address { Street = "8428 Blvd", City = "Phoenix", PostalCode = "88-112", ClientTypeId = clientTypes[4].Id },
                new Address { Street = "4968 Lane", City = "Philadelphia", PostalCode = "72-879", ClientTypeId = clientTypes[0].Id },
                new Address { Street = "3779 Ave", City = "San Antonio", PostalCode = "91-858", ClientTypeId = clientTypes[1].Id },
                new Address { Street = "8746 St", City = "San Diego", PostalCode = "74-604", ClientTypeId = clientTypes[2].Id },
                new Address { Street = "5693 Lane", City = "Dallas", PostalCode = "36-519", ClientTypeId = clientTypes[3].Id },
                new Address { Street = "7328 Lane", City = "San Jose", PostalCode = "11-602", ClientTypeId = clientTypes[4].Id },
                new Address { Street = "2392 Blvd", City = "Austin", PostalCode = "23-924", ClientTypeId = clientTypes[0].Id },
                new Address { Street = "8338 Ave", City = "Jacksonville", PostalCode = "86-538", ClientTypeId = clientTypes[1].Id },
                new Address { Street = "856 Lane", City = "San Francisco", PostalCode = "80-895", ClientTypeId = clientTypes[2].Id },
                new Address { Street = "8951 Lane", City = "Columbus", PostalCode = "85-764", ClientTypeId = clientTypes[3].Id },
                new Address { Street = "7553 Ave", City = "Fort Worth", PostalCode = "75-489", ClientTypeId = clientTypes[4].Id },
                new Address { Street = "7283 Ave", City = "Indianapolis", PostalCode = "10-673", ClientTypeId = clientTypes[0].Id },
                new Address { Street = "3894 Rd", City = "Charlotte", PostalCode = "63-223", ClientTypeId = clientTypes[1].Id },
                new Address { Street = "5361 Blvd", City = "Seattle", PostalCode = "19-319", ClientTypeId = clientTypes[2].Id },
                new Address { Street = "3952 St", City = "Denver", PostalCode = "50-355", ClientTypeId = clientTypes[3].Id },
                new Address { Street = "5722 Rd", City = "Washington D.C.", PostalCode = "49-622", ClientTypeId = clientTypes[4].Id }
            };

            await context.Addresses.AddRangeAsync(addresses);
            await context.SaveChangesAsync();
        }
    }
}