using LgpArch.Facades.Repositories;

namespace LgpArch.Database.Repositories;

public class WeatherRepository : IWeatherRepository
{
    public WeatherRepository(LgpArchDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public LgpArchDbContext DbContext { get; }

    public async Task<IEnumerable<IWeather>> GetWeathersAsync(DateTime minDate, DateTime maxDate = default)
    {
        if (maxDate == default)
        {
            minDate = minDate.Date;
            maxDate = minDate.AddDays(1);
        }
        return await DbContext.Weathers
            .Where(d => d.Date >= minDate && d.Date < maxDate)
            .ToListAsync();
    }

    public async Task<IWeather> SetWeatherAsync(IWeather weather)
    {
        var response = DbContext.Weathers.Add(new BusinessObjects.Weather
        {
            Date = weather.Date,
            TemperatureC = weather.TemperatureC,
            Summary = weather.Summary
        }).Entity;
        await DbContext.SaveChangesAsync();
        return response;
    }
    public async Task<IEnumerable<IWeather>> SetWeatherAsync(IEnumerable<IWeather> weathers)
    {
        var list = weathers.Select(weather => new BusinessObjects.Weather
        {
            Date = weather.Date,
            TemperatureC = weather.TemperatureC,
            Summary = weather.Summary
        });
        DbContext.Weathers.AddRange(list);
        await DbContext.SaveChangesAsync();
        return list.AsEnumerable<IWeather>();
    }
}
