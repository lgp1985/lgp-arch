namespace TestSampleApi;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        // Arrange
        var weatherForecast = new WeatherForecastController(new NullLogger<WeatherForecastController>());

        // Act
        var response = weatherForecast.Get();

        // Assert
        Assert.AreEqual(5, response.Count());
    }
}