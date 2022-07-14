namespace LgpArch.Database;

public class LgpArchDbContext : DbContext
{
    internal LgpArchDbContextOptions Options { get; }
    public LgpArchDbContext() : base() { }
    public LgpArchDbContext(Microsoft.Extensions.Options.IOptions<LgpArchDbContextOptions> optionsAccessor, DbContextOptions<LgpArchDbContext> options) : base(options)
    {
        Options = optionsAccessor.Value;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Options.ConnectionString, sqlServerDbContextOptionsBuilder =>
        {
            sqlServerDbContextOptionsBuilder.MigrationsAssembly(System.Reflection.Assembly.GetExecutingAssembly().FullName);
        });
    }

    public DbSet<BusinessObjects.Weather> Weathers { get; set; }
}