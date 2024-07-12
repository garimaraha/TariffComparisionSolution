using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffComparisionModel.Products;

namespace TariffComparisionModel.Test.Products
{
    public class PackagedTariffProductTest
    {
        public readonly ITariffProduct _tiffProduct;
        public PackagedTariffProductTest()
        {
            _tiffProduct = new PackagedTariffProduct();
        }
        [Fact]
        public void GetAnnualCost_Given3500_Should_ReturnAnnualCost800()
        {
            decimal actualannualCost = _tiffProduct.AnnualCostCalculation(3500);
            decimal expectedAnnualCost = 800;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        [Fact]
        public void GetAnnualCost_Given4500_Should_ReturnAnnualCost950()
        {
            decimal actualannualCost = _tiffProduct.AnnualCostCalculation(4500);
            decimal expectedAnnualCost = 950;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
        [Fact]
        public void GetAnnualCost_Given6000_Should_ReturnAnnualCost1400()
        {
            decimal actualannualCost = _tiffProduct.AnnualCostCalculation(6000);
            decimal expectedAnnualCost = 1400;
            expectedAnnualCost.Should().Be(actualannualCost);

        }

        [Fact]//Test for Boundary values
        public void GetAnnualCost_Given4001_Should_ReturnAnnualCost()
        {
            decimal actualannualCost = _tiffProduct.AnnualCostCalculation(4001);
            decimal expectedAnnualCost = 800.30m;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
       

        [Fact]
        public void GetAnnualCost_Given4500_Should_ReturnAnnualCost1400_WithDiffrentTariffCalculation()
        {
            var tiffProduct = new PackagedTariffProduct(4000, 800, .40m);
            decimal actualannualCost = tiffProduct.AnnualCostCalculation(4500);
            decimal expectedAnnualCost = 1000;
            expectedAnnualCost.Should().Be(actualannualCost);

        }
    }
}
