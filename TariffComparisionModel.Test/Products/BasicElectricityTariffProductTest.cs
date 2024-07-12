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
        [Fact]
        public void GetAnnualCost_Given3500_Should_ReturnAnnualCost830()
        {
            decimal actualannualCost = _tiffProduct.AnnualCostCalculation(3500);
            decimal expectedAnnualCost = 830;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        [Fact]
        public void GetAnnualCost_Given4500_Should_ReturnAnnualCost1050()
        {
            decimal actualannualCost = _tiffProduct.AnnualCostCalculation(4500);
            decimal expectedAnnualCost = 1050;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        [Fact]
        public void GetAnnualCost_Given6000_Should_ReturnAnnualCost1380()
        {
            decimal actualannualCost = _tiffProduct.AnnualCostCalculation(6000);
            decimal expectedAnnualCost = 1380;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        [Fact]
        public void GetAnnualCost_Given3000_Should_ReturnAnnualCost960_WithDiffrentTariffCalculation()
        {
            var tiffProduct = new BasicElectricityTariffProduct(5, .3m);
            decimal actualannualCost = tiffProduct.AnnualCostCalculation(3000);
            decimal expectedAnnualCost = 960;
            expectedAnnualCost.Should().Be(actualannualCost);


        }
    }

 }
