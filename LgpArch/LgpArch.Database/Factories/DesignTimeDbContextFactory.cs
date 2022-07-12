using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LgpArch.Database.Repositories.Factories;

/// <summary>
/// Files db context factory used by the EF (entity framework) tool to create database migrations, etc.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LgpArchDbContext>
{
    public LgpArchDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LgpArchDbContext>();

        if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
        {
            optionsBuilder.UseSqlServer("Server=sqldata;Database=Files;User Id=sa;Password=Pass@word;", sqlServerOptionsAction: o => o.MigrationsAssembly(this.GetType().Assembly.GetName().Name));
        }
        else
        {
            optionsBuilder.UseSqlServer("Server=tcp:127.0.0.1,5443;Database=Files;User Id=sa;Password=Pass@word;", sqlServerOptionsAction: o => o.MigrationsAssembly(this.GetType().Assembly.GetName().Name));
        }

        return new LgpArchDbContext(optionsBuilder.Options);

    }
}
