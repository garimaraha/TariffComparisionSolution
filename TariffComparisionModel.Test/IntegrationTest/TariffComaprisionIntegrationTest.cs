using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace TariffComparisionModel.Test.IntegrationTest
{
    public class TariffComaprisionIntegrationTest :IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly WebApplicationFactory<Program> _factory;

        public TariffComaprisionIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        /*Negative Network level Integration Testing -- to ennsure app's components function 
        correctly at a level that includes the app's supporting infrastructure,such as network */
        [Theory]
        [InlineData("abc")] // Invalid input: non-numeric value
        [InlineData("")]    // Invalid input: empty value
        [InlineData(null)]  // Invalid input: null value
        [InlineData("$*")]  // Invalid input: having special characters
        public async Task GetTariffComparisons_Should_Not_Accept_Invalid_Query_Parameter(string input)
        {

            // Prepare Client
            var client = _factory.CreateClient();
            var apiUrl = $"/api/TariffComparision/compareCosts?Consumption (kwh/year)={input}";

            // Call API with unpexpected query parameters
            var response = await client.GetAsync(apiUrl);

            // Fluent Assertion
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
