namespace LgpArch.Facades.BusinessObjects;
public interface IWeather
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
}