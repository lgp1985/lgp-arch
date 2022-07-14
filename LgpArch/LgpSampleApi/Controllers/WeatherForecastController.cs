using LgpArch.Facades.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LgpSampleApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> logger;
    private readonly IWeatherRepository weatherRepository;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherRepository weatherRepository)
    {
        this.logger = logger;
        this.weatherRepository = weatherRepository;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost]
    public async Task<IEnumerable<LgpArch.Facades.BusinessObjects.IWeather>> Set(IEnumerable<WeatherForecast> weathers)
    {
        //var response =  await Task.WhenAll(weathers.Select(weather => weatherRepository.SetWeatherAsync(weather)));
        var response = await weatherRepository.SetWeatherAsync(weathers.AsEnumerable<LgpArch.Facades.BusinessObjects.IWeather>());
        return response;
    }
}