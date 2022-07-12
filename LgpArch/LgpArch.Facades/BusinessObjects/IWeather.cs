namespace LgpArch.Facades.BusinessObjects;
public interface IWeather
{
    public DateTime Date { get; }
    public int TemperatureC { get; }
    public int TemperatureF { get; }
    public string? Summary { get; }
}