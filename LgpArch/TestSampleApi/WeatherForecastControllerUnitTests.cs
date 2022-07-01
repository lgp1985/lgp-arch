namespace TestSampleApi;

[TestClass]
public class WeatherForecastControllerUnitTests
{
    [TestMethod]
    public void IsResponseWith5Results()
    {
        // Arrange
        var weatherForecast = new WeatherForecastController(new NullLogger<WeatherForecastController>());

        // Act
        var response = weatherForecast.Get();

        // Assert
        Assert.AreEqual(5, response.Count());
    }

    [TestMethod]
    public void ResponseContainsValuesResults()
    {
        // Arrange
        var weatherForecast = new WeatherForecastController(new NullLogger<WeatherForecastController>());

        // Act
        var response = weatherForecast.Get();

        // Assert
        foreach (var item in response)
        {
            Assert.IsTrue(item.TemperatureC != item.TemperatureF);
            Assert.IsNotNull(item.Summary);
            Assert.IsNotNull(item.Date);
            Assert.IsTrue(DateTime.MinValue < item.Date);
        }
    }
}