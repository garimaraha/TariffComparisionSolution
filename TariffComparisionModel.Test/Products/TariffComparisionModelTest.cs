using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TariffComparisionModel;
using TariffComparisionModel.Services;
using TariffComparisionModel.Factories;
using TariffComparisionModel.Model;
using FluentAssertions;
using TariffComparisionModel.Test.SampleData;

namespace TariffComparisionModel.Test.Products
{
    public class TariffComparisionModelTest
    {
       
        private readonly ITariffComparisionService tariffComparisionService;
        private readonly ITariffComparisionFactory  tariffComparisionFactory;
        public TariffComparisionModelTest()
        {
            tariffComparisionFactory = new TariffComparisionFactory();
            tariffComparisionService = new TariffComparisionService(tariffComparisionFactory);
        }

        [Theory]
        [InlineData(3500)]
        [InlineData(4500)]
        [InlineData(6000)]
        [InlineData(0)]
        public void GetComparedProducts_WithMultipleConsumptionUnit_ReturnResponseTariffDTOs_WithCorrectAnnualCost(decimal consumption)
        {
            List<TariffCost> actualtariffCosts = tariffComparisionService.GetComparedProducts(consumption).ToList();

            List<TariffCost> expectedTariffList = GetAllTariffsForGivenConsumptionTestDataStore.CreateTariffsForGivenConsumption(consumption).OrderBy(o=>o.AnnualCosts).ToList();

            actualtariffCosts.Should().BeEquivalentTo(expectedTariffList,options=>options.WithStrictOrdering());

        }
        [Theory]
        [InlineData(3500)]
        [InlineData(4500)]
        [InlineData(6000)]
        [InlineData(0)]
        public void GetComparedProducts_WithMultipleConsumptionUnit_ReturnResponseTariffDTOs_TariffWithAnnualCostInAcsendingOrder(decimal consumption)
        {
            IEnumerable<TariffCost> tariffCosts = tariffComparisionService.GetComparedProducts(consumption);

            var expectedBasicTariffAnnualCost = BasicElectricityTariffTestDataStore.sampleTestDataList.Find(annualCost => annualCost.ConsumptionKWHPerYear == consumption)?.AnnualCost;
            var expectedPackagedTariffAnnualCost = PackagedTariffTestDataStore.sampleTestDataList.Find(annualCost => annualCost.ConsumptionKWHPerYear == consumption)?.AnnualCost;

            var actualBasicTariffAnnualCost = tariffCosts.FirstOrDefault(tariff => tariff.TariffName == BasicElectricityTariffTestDataStore.sampleTestDataList.First().TariffName)?.AnnualCosts;
            var actualPackagedTariffAnnualCost = tariffCosts.FirstOrDefault(tariff => tariff.TariffName == PackagedTariffTestDataStore.sampleTestDataList.First().TariffName)?.AnnualCosts;

            actualBasicTariffAnnualCost.Should().Be(expectedBasicTariffAnnualCost);
            actualPackagedTariffAnnualCost.Should().Be(expectedPackagedTariffAnnualCost);

        }
        //Returned Tariff Comaprision service  should have all avaiable Tariffs
        [Theory]
        [InlineData(3500)]
        [InlineData(4500)]
        [InlineData(6000)]
        [InlineData(0)]
        public void GetComparedProducts_WithMultipleConsumptionUnit_ReturnResponseTariffDTOs_VerifyAllAvailableTariffs(decimal consumption)
        {  
            List<string> actualtariffCosts = tariffComparisionService.GetComparedProducts(consumption).Select(tariff=>tariff.TariffName).ToList();

            List<string> expectedTariffList = GetAllTariffsForGivenConsumptionTestDataStore.CreateTariffsForGivenConsumption(consumption).Select(tariff=>tariff.TariffName).ToList();

            actualtariffCosts.Should().BeEquivalentTo(expectedTariffList);

            


        }
    }
}
