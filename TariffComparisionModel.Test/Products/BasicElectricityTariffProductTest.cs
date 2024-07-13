using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffComparisionModel.Products;
using Xunit;

namespace TariffComparisionModel.Test.Products
{
    public class BasicElectricityTariffProductTest
    {
        public readonly ITariffProduct _tiffProduct;
        public BasicElectricityTariffProductTest()
        {
            _tiffProduct = new BasicElectricityTariffProduct();
        }
        /// <summary>
        /// Tests that the GetAnnualCost method returns an annual cost of 830 
        /// when provided with a consumption value of 3500 kWh/year. 
        /// </summary>
        [Fact]
        public async Task GetAnnualCost_Given3500_Should_ReturnAnnualCost830()
        {
            decimal actualannualCost = await Task.FromResult(_tiffProduct.AnnualCostCalculation(3500));
            decimal expectedAnnualCost = 830;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        /// <summary>
        /// Tests that the GetAnnualCost method returns an annual cost of 1050
        /// when provided with a consumption value of 4500 kWh/year.
        /// </summary>
        [Fact]
        public async Task GetAnnualCost_Given4500_Should_ReturnAnnualCost1050()
        {
            decimal actualannualCost = await Task.FromResult(_tiffProduct.AnnualCostCalculation(4500));
            decimal expectedAnnualCost = 1050;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        /// <summary>
        /// Tests that the GetAnnualCost method returns an annual cost of 1380 
        /// when provided with a consumption value of 6000 kWh/year.
        /// </summary>

        [Fact]
        public async Task GetAnnualCost_Given6000_Should_ReturnAnnualCost1380()
        {
            decimal actualannualCost = await Task.FromResult(_tiffProduct.AnnualCostCalculation(6000));
            decimal expectedAnnualCost = 1380;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        /// <summary>
        /// Tests that the GetAnnualCost method returns an annual cost of 960 
        /// when provided with a consumption value of 3000 kWh/year, 
        /// using a different tariff calculation method.
        /// </summary>

        [Fact]
        public async Task GetAnnualCost_Given3000_Should_ReturnAnnualCost960_WithDiffrentTariffCalculation()
        {
            var tiffProduct = await Task.FromResult( new BasicElectricityTariffProduct(5,0.3m));
            decimal actualannualCost = tiffProduct.AnnualCostCalculation(3000);
            decimal expectedAnnualCost = 960;
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
