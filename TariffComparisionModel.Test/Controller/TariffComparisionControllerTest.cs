using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Newtonsoft.Json.Serialization;
using TariffComparisionModel.Services;
using TariffComparisionModel.API;
using TariffComparisionModel.API.Controllers;
using TariffComparisionModel.Model;
using TariffComparisionModel.API.DTOs;
using TariffComparisionModel.API.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using FluentAssertions;
using Xunit.Sdk;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TariffComparisionModel.Test.Controller
{
  
    public class TariffComparisionControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly Mock<ITariffComparisionService> _tariffSvcMock;
        private readonly TariffComparisionController _controller;
        private readonly WebApplicationFactory<Program> _factory;

        public TariffComparisionControllerTest(WebApplicationFactory<Program> factory)
        {
            _tariffSvcMock = new Mock<ITariffComparisionService>();
            _tariffSvcMock.Setup(tariff => tariff.GetComparedProducts(It.IsAny<decimal>())).Returns(CreateTariffs_SampleTestData);
            _controller = new TariffComparisionController(_tariffSvcMock.Object);
            _factory = factory;
        }
        private IEnumerable<TariffCost> CreateTariffs_SampleTestData()
        {
            yield return new TariffCost
            {
               TariffName= "Packaged Tariff",
               AnnualCosts=800
            };

            yield return new TariffCost
            {
                TariffName = "Packaged Tariff",
                AnnualCosts = 850
            };
        }

        [InlineData(3500)]
        [InlineData(3500.56)]
        [Theory]
        public void GetTariffComparisons_Should_Accept_Number_In_ConsumptionkWhPerYear_ReturnResponseTariffDTOs(decimal consumption)
        {

            var tariffList = _controller.GetTariffComparisons(new ConsumptionRequestDTO() { Consumption = consumption });
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(tariffList.Result);
            var tariffDtos = Assert.IsAssignableFrom<IEnumerable<ResponseTariffDTO>>(objectResult.Value).AsEnumerable();

            var expectedList = CreateTariffs_SampleTestData().ConvertToDto().OrderBy(o => o.AnnualCosts);

            tariffDtos.Should().BeEquivalentTo(expectedList);
              
        }
        [Theory]
        [InlineData(4500)]      
        public void GetTariffComparisons_With_ValidInput_ConsumptionkWhPerYear_ReturnResponseTariffDTOs_With_TariffName_AnnualCost(decimal consumption)
        {
            var tariffList = _controller.GetTariffComparisons(new ConsumptionRequestDTO() { Consumption = consumption });
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(tariffList.Result);
            var tariffDtos = Assert.IsAssignableFrom<IEnumerable<ResponseTariffDTO>>(objectResult.Value).AsEnumerable();

            var expectedList = CreateTariffs_SampleTestData().ConvertToDto().OrderBy(o => o.AnnualCosts);

            tariffDtos.Should().BeEquivalentTo(expectedList, options => options.WithStrictOrdering());


        }
        [InlineData(-5000)] //Invalid Input -- Negative integer number
        [InlineData(-5000.55)] //Invalid Input -- Negative decimal number
        [Theory]
        public void GetTariffComparisons_Should_Not_Accept_Negative_Number_In_ConsumptionkWhPerYear_ReturnStatus400BadRequest(decimal consumption)
        {
            var tariffList = _controller.GetTariffComparisons(new ConsumptionRequestDTO() { Consumption = consumption });
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(tariffList.Result);
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
     
        [Fact]
        public void GetTariffComparisons_Should_Not_Accept_EmptyModel_For_ConsumptionkWhPerYear_Return_Status400BadRequest()
        {
            var expectederrorMessage = "Required Consumption field is not provided.";
            _controller.ModelState.AddModelError("Consumption", expectederrorMessage);//Assiging custom model state error for true assertion
            var emptydModel = new ConsumptionRequestDTO();
            // Act
            var result = _controller.GetTariffComparisons(emptydModel); // Need to Pass Empty Model, check Model state working fine 

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);

            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

            var error = Assert.IsType<SerializableError>(badRequestResult.Value);
            var actualerrorMessage = ((string[])error.ToList().Find(e => e.Key == "Consumption").Value)[0];

            expectederrorMessage.Should().Be(actualerrorMessage);  


        }
        [Fact]
        public void GetTariffComparisons_Service_Throws_Exception_Return500InternalServerError()
        {

            _tariffSvcMock.Setup(tariff => tariff.GetComparedProducts(It.IsAny<decimal>())).Throws(new Exception());

            // Act
            var result = _controller.GetTariffComparisons(new ConsumptionRequestDTO() { Consumption=4500}); // Need to Pass Empty Model, check Model state working fine 

            // Assert
            var badRequestResult = Assert.IsType<StatusCodeResult>(result.Result);

            badRequestResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

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
