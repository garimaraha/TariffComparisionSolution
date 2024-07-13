using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TariffComparisionModel.API.DTOs;
using TariffComparisionModel.Products;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TariffComparisionModel.Test.Products
{
    public class PackagedTariffProductTest
    {
        public readonly ITariffProduct _tiffProduct;
        public PackagedTariffProductTest()
        {
            _tiffProduct = new PackagedTariffProduct();
        }
        /// <summary>
        /// Tests that the GetAnnualCost method returns an annual cost of 800 
        /// when provided with a consumption value of 3500 kWh/year.
        /// </summary>
        [Fact]
        public async Task GetAnnualCost_Given3500_Should_ReturnAnnualCost800()
        {
            decimal actualannualCost = await Task.FromResult(_tiffProduct.AnnualCostCalculation(3500));
            decimal expectedAnnualCost = 800;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        /// <summary>
        /// Tests that the GetAnnualCost method returns an annual cost of 950 
        /// when provided with a consumption value of 4500 kWh/year.
        /// </summary>

        [Fact]
        public async Task GetAnnualCost_Given4500_Should_ReturnAnnualCost950()
        {
            decimal actualannualCost = await Task.FromResult(_tiffProduct.AnnualCostCalculation(4500));
            decimal expectedAnnualCost = 950;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        /// <summary>
        /// Tests that the GetAnnualCost method returns an annual cost of 1400 
        /// when provided with a consumption value of 6000 kWh/year.
        /// </summary>
        [Fact]
        public async Task GetAnnualCost_Given6000_Should_ReturnAnnualCost1400()
        {
            decimal actualannualCost = await Task.FromResult(_tiffProduct.AnnualCostCalculation(6000));
            decimal expectedAnnualCost = 1400;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        /// <summary>
        /// Tests that the GetAnnualCost method returns the expected annual cost 
        /// when provided with a consumption value of 4000.5 kWh/year, 
        /// specifically testing the boundary condition.
        /// </summary>

        [Fact]
        public async Task GetAnnualCost_Given4000_5_Should_ReturnAnnualCost_TestForBoundaryValue()
        {
            decimal actualannualCost = await Task.FromResult(_tiffProduct.AnnualCostCalculation(4000.5m));
            decimal expectedAnnualCost = 800.150m;
            expectedAnnualCost.Should().Be(actualannualCost);

        }

        /// <summary>
        /// Tests that the GetAnnualCost method returns an annual cost of 1400 
        /// when provided with a consumption value of 4500 kWh/year, 
        /// using a different tariff calculation method.
        /// </summary>

        [Fact]
        public async Task GetAnnualCost_Given4500_Should_ReturnAnnualCost1400_WithDiffrentTariffCalculation()
        {
            var tiffProduct = new PackagedTariffProduct(4000, 800, .40m);
            decimal actualannualCost = await Task.FromResult(tiffProduct.AnnualCostCalculation(4500));
            decimal expectedAnnualCost = 1000;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        /// <summary>
        /// Tests that the GetAnnualCost method throws a custom exception 
        /// when provided with a negative consumption value.
        /// </summary>
        /// <param name="consumption"></param>
        /// <returns></returns>
        [InlineData(-5000)] //Invalid Input -- Negative integer number
        [InlineData(-5000.55)] //Invalid Input -- Negative decimal number
        [Theory]
        public void GetAnnualCost_Given_Negative_Value_Should_ThrowCustomException(decimal consumption)
        {
            Action act = () => _tiffProduct.AnnualCostCalculation(consumption);
            act.Should().Throw<ArgumentException>()
                .WithParameterName(nameof(consumption));

        }
    }
}
