namespace LgpArch.Database.BusinessObjects;

public class Weather : IWeather
{
    [System.ComponentModel.DataAnnotations.Key]
    public DateTime Date { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public int TemperatureC { get; set; }

    [System.ComponentModel.DataAnnotations.MaxLength(32)]
    public string? Summary { get; set; }
}
