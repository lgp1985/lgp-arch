using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace TestSampleApi
{
    /// <summary>
    /// These tests are created with below docs as reference
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0#project-file-csproj"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/test/middleware?view=aspnetcore-6.0#send-requests-with-httpclient"/>
    /// </summary>
    [TestClass]
    public class MiddlewareUnitTests
    {
        public static HttpClient Client { get { return new WebApplicationFactory<Program>().CreateClient(); } }

        [TestMethod]
        public async Task CheckUnauthorizedRouteAsync()
        {
            await Client.CheckStatusForPath("/WeatherForecast", HttpStatusCode.Unauthorized);
        }

        [TestMethod]
        public async Task CheckNotFoundRouteAsync()
        {
            await Client.CheckStatusForPath("/", HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task CheckOKRouteAsync()
        {
            await Client.CheckStatusForPath("/swagger/v1/swagger.json", HttpStatusCode.OK);
        }
    }
    public static class Extensions
    {
        public static async Task CheckStatusForPath(this HttpClient client, string route, HttpStatusCode expectedStatus)
        {
            var response = await client.GetAsync(route);
            Assert.AreEqual(expectedStatus, response.StatusCode);
        }
    }
}
