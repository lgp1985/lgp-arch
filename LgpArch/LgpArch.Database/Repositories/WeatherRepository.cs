﻿using LgpArch.Facades.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<IWeather> SetWeatherAsync(IWeather weather) => (await DbContext.Weathers.AddAsync(new BusinessObjects.Weather
    {
        Date = weather.Date,
        TemperatureC = weather.TemperatureC,
        Summary = weather.Summary
    })).Entity;
}
