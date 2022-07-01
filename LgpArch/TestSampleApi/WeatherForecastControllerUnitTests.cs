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
}