using LgpArch.Facades.BusinessObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LgpArch.Facades.Repositories;

public interface IWeatherRepository
{
    /// <summary>
    /// Gets all temperatures across a time range, from minDate to maxDate.
    /// </summary>
    /// <remarks>If maxDate isn't supplied, returns all temperatures of the givend day.</remarks>
    /// <param name="minDate">Start date and time</param>
    /// <param name="maxDate">End date and time of the range, if not supplied returs records of the given day.</param>
    /// <returns>Array of Weather conditions.</returns>
    public Task<IEnumerable<IWeather>> GetWeathersAsync(DateTime minDate, DateTime maxDate = default);

    /// <summary>
    /// Saves the provided weather into the database.
    /// </summary>
    /// <param name="weather">Current weather</param>
    public ValueTask<EntityEntry<IWeather>> SetWeatherAsync(IWeather weather);
}
