using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
            var apiUrl = $"/api/TariffComparision/compareCosts?Consumption={input}";

            // Call API with unpexpected query parameters
            var response = await client.GetAsync(apiUrl);

            // Fluent Assertion
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
