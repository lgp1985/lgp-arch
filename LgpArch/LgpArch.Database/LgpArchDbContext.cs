namespace LgpArch.Database;

public class LgpArchDbContext : DbContext
{
    public DbSet<IWeather> Weathers { get; set; }
}