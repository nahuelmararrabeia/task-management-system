using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Tests.Integration.Factories;
using Testcontainers.PostgreSql;

namespace TaskManagement.Tests.Integration.Fixtures;

public class PostgresFixture : IAsyncLifetime
{
    public PostgreSqlContainer Container { get; private set; } = default!;
    public CustomWebApplicationFactory Factory { get; private set; } = default!;
    public HttpClient Client { get; private set; } = default!;
    public IServiceProvider Services => Factory.Services;

    public string ConnectionString => Container.GetConnectionString();

    public async Task InitializeAsync()
    {
        Container = new PostgreSqlBuilder("postgres:16-alpine")
            .WithDatabase("task_management_test")
            .WithUsername("postgres")
            .WithPassword("admin")
            .Build();

        await Container.StartAsync();
        await ApplyMigrationsAsync();

        Factory = new CustomWebApplicationFactory(ConnectionString);
        Client = Factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await Container.DisposeAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        await Container.ExecScriptAsync(@"
            TRUNCATE TABLE ""Tasks"" RESTART IDENTITY CASCADE;");
    }

    private async Task ApplyMigrationsAsync()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        using var context = new AppDbContext(options);

        await context.Database.MigrateAsync();
    }
}