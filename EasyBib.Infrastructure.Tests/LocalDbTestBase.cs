using EasyBib.Infrastructure;
using EasyBib.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EasyBib.Infrastructure.Tests;

public abstract class LocalDbTestBase : IAsyncLifetime
{
    private readonly string _databaseName = $"EasyBibTests_{Guid.NewGuid():N}";

    private DbContextOptions<EasyBibDbContext> _options = null!;

    /// <summary>UnitOfWork-Kontext für den gesamten Test (inkl. Change-Tracking).</summary>
    protected EasyBibDbContext Context { get; private set; } = null!;

    /// <summary>Frischer Kontext ohne Change-Tracker-Cache – nur für Asserts gegen die echte DB.</summary>
    protected EasyBibDbContext CreateVerifyContext() => new(_options);

    public async Task InitializeAsync()
    {
        _options = new DbContextOptionsBuilder<EasyBibDbContext>()
            .UseSqlServer(
                $"Server=(localdb)\\MSSQLLocalDB;Database={_databaseName};" +
                "Trusted_Connection=True;MultipleActiveResultSets=true")
            .Options;

        Context = new EasyBibDbContext(_options);
        await Context.Database.MigrateAsync(); // inkl. HasData-Seed
    }

    public async Task DisposeAsync()
    {
        await Context.DisposeAsync();
        await using var cleanup = CreateVerifyContext();
        await cleanup.Database.EnsureDeletedAsync();
    }
}