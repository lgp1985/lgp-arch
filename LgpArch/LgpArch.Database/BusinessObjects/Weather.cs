using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgpArch.Database.BusinessObjects;

public record Weather(DateTime Date, int TemperatureC, string? Summary) : IWeather
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
