using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Net;

namespace TestSampleApi
{
    [TestClass]
    public class MiddlewareUnitTests
    {
        [TestMethod]
        public async Task CheckIfAuthenticationIsInPlaceAsync()
        {
            // Arrange

            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseTestServer();
                });

            var client = application.CreateClient();

            // Act / Assert

            await client.CheckStatusForPath("/WeatherForecast", HttpStatusCode.Unauthorized);
            await client.CheckStatusForPath("/", HttpStatusCode.NotFound);
        }

    }
    public static class Extensions{
        public static async Task CheckStatusForPath(this HttpClient client, string route, HttpStatusCode expectedStatus)
        {
            await Assert.ThrowsExceptionAsync<HttpRequestException>(async () =>
            {
                try
                {
                    var response = await client.GetStringAsync(route);
                }
                catch (HttpRequestException ex)
                {
                    Assert.AreEqual(expectedStatus, ex.StatusCode);
                    throw;
                }
            });
        }
    }
}
