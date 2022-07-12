namespace LgpArch.Database;

public class LgpArchDbContext : DbContext
{
    public LgpArchDbContext() : base()
    {
    }
    public LgpArchDbContext(DbContextOptions<LgpArchDbContext> options) : base(options)
    {
    }
    public DbSet<BusinessObjects.Weather> Weathers { get; set; }
}