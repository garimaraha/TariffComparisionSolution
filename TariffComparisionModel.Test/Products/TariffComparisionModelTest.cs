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
        /// <summary>
        /// Tests that the GetComparedProducts method returns the correct list of Tariff Costs 
        /// with accurate annual costs when provided with multiple consumption units.
        /// </summary>
        [Theory]
        [InlineData(3500)]
        [InlineData(4500)]
        [InlineData(6000)]
        [InlineData(0)]
        public async Task GetComparedProducts_WithMultipleConsumptionUnit_ReturnTariffCosts_WithCorrectAnnualCost(decimal consumption)
        {
            List<TariffCost> actualtariffCosts = await Task.FromResult(tariffComparisionService.GetComparedProducts(consumption).ToList());

            List<TariffCost> expectedTariffList = GetAllTariffsForGivenConsumptionTestDataStore.CreateTariffsForGivenConsumption(consumption).OrderBy(o=>o.AnnualCosts).ToList();

            actualtariffCosts.Should().BeEquivalentTo(expectedTariffList,options=>options.WithStrictOrdering());

        }
        /// <summary>
        /// Tests that the GetComparedProducts method returns a list of Tariff Costs 
        /// sorted in ascending order by annual cost when provided with multiple consumption units.
        /// </summary>
        [Theory]
        [InlineData(3500)]
        [InlineData(4500)]
        [InlineData(6000)]
        [InlineData(0)]
        public async Task GetComparedProducts_WithMultipleConsumptionUnit_ReturnTariffCosts_TariffWithAnnualCostInAcsendingOrder(decimal consumption)
        {
            IEnumerable<TariffCost> tariffCosts = await Task.FromResult(tariffComparisionService.GetComparedProducts(consumption));

            var expectedBasicTariffAnnualCost = BasicElectricityTariffTestDataStore.sampleTestDataList.Find(annualCost => annualCost.ConsumptionKWHPerYear == consumption)?.AnnualCost;
            var expectedPackagedTariffAnnualCost = PackagedTariffTestDataStore.sampleTestDataList.Find(annualCost => annualCost.ConsumptionKWHPerYear == consumption)?.AnnualCost;

            var actualBasicTariffAnnualCost = tariffCosts.FirstOrDefault(tariff => tariff.TariffName == BasicElectricityTariffTestDataStore.sampleTestDataList.First().TariffName)?.AnnualCosts;
            var actualPackagedTariffAnnualCost = tariffCosts.FirstOrDefault(tariff => tariff.TariffName == PackagedTariffTestDataStore.sampleTestDataList.First().TariffName)?.AnnualCosts;

            actualBasicTariffAnnualCost.Should().Be(expectedBasicTariffAnnualCost);
            actualPackagedTariffAnnualCost.Should().Be(expectedPackagedTariffAnnualCost);

        }
        /// <summary>
        /// Tests that the GetComparedProducts method returns tariff costs 
        /// and verifies that all available tariffs are included in the response 
        /// when provided with multiple consumption units.
        /// </summary>

        [Theory]
        [InlineData(3500)]
        [InlineData(4500)]
        [InlineData(6000)]
        [InlineData(0)]
        public async Task GetComparedProducts_WithMultipleConsumptionUnit_ReturnTariffCosts_VerifyAllAvailableTariffs(decimal consumption)
        {  
            List<string> actualtariffCosts = await Task.FromResult(tariffComparisionService.GetComparedProducts(consumption).Select(tariff => tariff.TariffName).ToList());

            List<string> expectedTariffList = GetAllTariffsForGivenConsumptionTestDataStore.CreateTariffsForGivenConsumption(consumption).Select(tariff=>tariff.TariffName).ToList();

            actualtariffCosts.Should().BeEquivalentTo(expectedTariffList);
        }
    }
}
