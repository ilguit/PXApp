using PXApp.Common.AppConfig;
using PXApp.Core.Db;
using PXApp.Core.Db.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace PXApp.Bootstrap;

internal static class ConfigureDb
{
    internal static IServiceCollection AddDb(this IServiceCollection services, IConfiguration config)
    {
        var dbConfig = config.GetSection(DatabaseSettings.ConfigSection).Get<DatabaseSettings>();
        if (string.IsNullOrWhiteSpace(dbConfig?.Endpoint))
        {
            throw new InvalidOperationException("DB endpoint is not set");
        }
        
        services.AddPooledDbContextFactory<PXAppDbContext>(options =>
        {
            var npgsqlBuilder = new NpgsqlDataSourceBuilder(dbConfig.GetConnectionString());
            npgsqlBuilder.EnableDynamicJson();
            
            options
                // .UseLazyLoadingProxies()
                .UseNpgsql(npgsqlBuilder.Build())
                .UseSnakeCaseNamingConvention();
        });

        return services;
    }
    
    internal static async Task<IServiceProvider> InitializeDb(this IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<PXAppDbContext>>();

        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        // await dbContext.SeedData();

        return services;
    }

    internal static async Task EnsureDatabaseIsUpToDate(this IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<PXAppDbContext>>();

        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            throw new InvalidOperationException(
                "The DB appears to be not up-to-date as there are pending migrations found");
        }
    }

    // private static async Task SeedData(this PXAppDbContext db)
    // {
    //     
    // }
}