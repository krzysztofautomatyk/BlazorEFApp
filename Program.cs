using MudBlazor.Services;
using BlazorEFApp.Components;
using BlazorEFApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BlazorEFApp.Domain.Repositories;
using BlazorEFApp.Domain.Entities;
using BlazorEFApp.Infrastructure.Data;
using MudBlazor;
using BlazorEFApp.Infrastructure.Repositories;
using BlazorEFApp.Domain.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
});


// Create directory for the database if it does not exist
//var databasePath = Path.Combine(builder.Environment.ContentRootPath, "Data");
//Directory.CreateDirectory(databasePath);

//var dbPath = Path.Combine(databasePath, "app.db");

var dbPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "Database", "todo.db");

// Register DbContext with DbContextFactory
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGenericRepository<Address>, GenericRepository<Address>>();
builder.Services.AddScoped<IGenericRepository<ClientType>, GenericRepository<ClientType>>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

// Add FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<AddressValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ClientTypeValidator>();

var app = builder.Build();

// Database initialization if flag in appsettings.json is set to true
using (var scope = app.Services.CreateScope())
{
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var shouldInitialize = configuration.GetValue<bool>("InitializeDatabase");
    await DatabaseSeeder.SeedDataIfEmpty(dbContextFactory, shouldInitialize);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
