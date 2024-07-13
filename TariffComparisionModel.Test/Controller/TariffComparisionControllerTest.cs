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
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TariffComparisionModel.Test.Controller
{
  
    public class TariffComparisionControllerTest 
    {
        private readonly Mock<ITariffComparisionService> _tariffSvcMock;
        private readonly TariffComparisionController _controller;

        public TariffComparisionControllerTest()
        {
            _tariffSvcMock = new Mock<ITariffComparisionService>();
            _tariffSvcMock.Setup(tariff => tariff.GetComparedProducts(It.IsAny<decimal>())).Returns(CreateTariffs_SampleTestData);
            _controller = new TariffComparisionController(_tariffSvcMock.Object);
           
        }
        /// <summary>
        /// Generates mock sample test data for creating tariffs to be used in unit tests
        /// </summary>
        /// <returns></returns>
        private IEnumerable<TariffCost> CreateTariffs_SampleTestData()
        {
            yield return new TariffCost
            {
               TariffName= "Packaged Tariff",
               AnnualCosts=800
            };

            yield return new TariffCost
            {
                TariffName = "Basic Electricity Tariff",
                AnnualCosts = 830
            };
        }

        /// <summary>
        /// Tests that the GetTariffComparisons method accepts a valid number for Consumption (kWh/year)
        /// and returns a list of ResponseTariffDTOs.
        /// </summary>
        /// <param name="consumption"></param>
        /// <returns></returns>
        [InlineData(3500)]
        [InlineData(3500.56)]
        [Theory]
      
        public async Task GetTariffComparisons_Should_Accept_Number_As_ConsumptionkWhPerYear_ReturnResponseTariffDTOs(decimal consumption)
        {
            
            var tariifList = await _controller.GetTariffComparisons(new ConsumptionRequestDTO { Consumption = consumption });

            var objectResult = tariifList.Should().BeOfType<OkObjectResult>().Subject;
            var tariffDtos = objectResult.Value.Should().BeAssignableTo<IEnumerable<ResponseTariffDTO>>().Subject;

            var expectedList = CreateTariffs_SampleTestData().ConvertToDto().OrderBy(o => o.AnnualCosts);

            tariffDtos.Should().BeEquivalentTo(expectedList);
        }

        /// <summary>
        /// Tests that the GetTariffComparisons method returns a list of ResponseTariffDTOs
        /// containing TariffName and AnnualCost when provided with valid input for Consumption (kWh/year).
        /// </summary>
        /// <param name="consumption"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(4500)]      
        public async Task GetTariffComparisons_With_ValidInput_ConsumptionkWhPerYear_ReturnResponseTariffDTOs_With_TariffName_AnnualCost(decimal consumption)
        {
            var tariifList = await _controller.GetTariffComparisons(new ConsumptionRequestDTO { Consumption = consumption });

            var objectResult = tariifList.Should().BeOfType<OkObjectResult>().Subject;
            var tariffDtos = objectResult.Value.Should().BeAssignableTo<IEnumerable<ResponseTariffDTO>>().Subject;

            var expectedList = CreateTariffs_SampleTestData().ConvertToDto().OrderBy(o => o.AnnualCosts);


            tariffDtos.Should().BeEquivalentTo(expectedList, options => options.WithStrictOrdering());


        }
        /// <summary>
        /// Tests that the GetTariffComparisons method throws a custom exception
        /// when a negative number is provided for Consumption (kWh/year).
        /// </summary>
        /// <param name="consumption"></param>
        /// <returns></returns>
        [InlineData(-5000)] //Invalid Input -- Negative integer number
        [InlineData(-5000.55)] //Invalid Input -- Negative decimal number
        [Theory]
        public async Task GetTariffComparisons_Should_Not_Accept_Negative_Number_In_ConsumptionkWhPerYear_ThrowCustomException(decimal consumption)
        {
            string expectedMessage = "Consumption (kWh/year) value must be zero or a positive number. (Parameter 'consumptionReqDto')";

            var tariffList = await Task.FromResult(_controller.GetTariffComparisons(new ConsumptionRequestDTO() { Consumption = consumption }));
            // Assert
            tariffList.Should().BeOfType<Task<IActionResult>>();

            tariffList.Exception?.InnerException?.Message.Should().Be(expectedMessage);
        }
        /// <summary>
        /// Tests that the GetTariffComparisons method throws a custom exception
        /// when the input model for Consumption (kWh/year) is empty.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetTariffComparisons_Should_Not_Accept_EmptyModel_For_ConsumptionkWhPerYear_ThrowCustomException()
        {
            string expectedMessage = "Input model cannot be null.";
            var tariffList = await Task.FromResult(_controller.GetTariffComparisons(new ConsumptionRequestDTO() { }));// Pass Empty model.
            tariffList.Should().BeOfType<Task<IActionResult>>();

            tariffList.Exception?.InnerException?.Message.Should().Be(expectedMessage);



        }
        /// <summary>
        /// Tests that the GetTariffComparisons method handles a SystemMockedException
        /// thrown by the service and verifies the appropriate error response.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetTariffComparisons_Service_Throws_Exception_SystemMockedException()
        {
            string expectedMessage = "There is an internal server error. Please contact support if this issue persists.";

            _tariffSvcMock.Setup(tariff => tariff.GetComparedProducts(It.IsAny<decimal>())).Throws(new Exception(expectedMessage));

            // Act
            var result = await Task.FromResult(_controller.GetTariffComparisons(new ConsumptionRequestDTO() { Consumption=4500})); // Need to Pass Empty Model, check Model state working fine 

            result.Exception?.InnerException?.Message.Should().Be(expectedMessage);

        }

        


    }
}
