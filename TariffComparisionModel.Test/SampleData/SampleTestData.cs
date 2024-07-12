using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffComparisionModel.Model;

namespace TariffComparisionModel.Test.SampleData
{
    public class SampleTestData
    {

        public decimal ConsumptionKWHPerYear {  get; set; }
        public string TariffName { get; set; } = "";
        public decimal AnnualCost {  get; set; }

               
    }
    public static class BasicElectricityTariffTestDataStore
    {
        public static List<SampleTestData> sampleTestDataList = new List<SampleTestData>
            {
                new SampleTestData { ConsumptionKWHPerYear = 3500, TariffName = "Basic Electricity Tariff",AnnualCost=830 },
                new SampleTestData { ConsumptionKWHPerYear = 4500, TariffName = "Basic Electricity Tariff",AnnualCost=1050 },
                new SampleTestData { ConsumptionKWHPerYear = 6000, TariffName = "Basic Electricity Tariff",AnnualCost=1380 },
                new SampleTestData { ConsumptionKWHPerYear = 0, TariffName = "Packaged Tariff",AnnualCost=60 }
            };
    }
    public static class PackagedTariffTestDataStore
    {
        public static List<SampleTestData> sampleTestDataList = new List<SampleTestData>
            {
                new SampleTestData { ConsumptionKWHPerYear = 3500, TariffName = "Packaged Tariff",AnnualCost=800 },
                new SampleTestData { ConsumptionKWHPerYear = 4500, TariffName = "Packaged Tariff",AnnualCost=950 },
                new SampleTestData { ConsumptionKWHPerYear = 6000, TariffName = "Packaged Tariff",AnnualCost=1400 },
                new SampleTestData { ConsumptionKWHPerYear = 0, TariffName = "Packaged Tariff",AnnualCost=800 }
            };
    }
    public static class GetAllTariffsForGivenConsumptionTestDataStore
    {
         public static IEnumerable<TariffCost> CreateTariffsForGivenConsumption(decimal consumption)
        {
            yield return new TariffCost
            {
                TariffName = "Packaged Tariff",
                //Expected Calculation model: 800€ for up to  4000kWh/year and above 4000kWh/year additionally 30 cent/kWh.
                AnnualCosts = 800 + ((consumption <= 4000) ? 0 : (consumption - 4000) * 0.30m)
            };

            yield return new TariffCost
            {
                TariffName = "Basic Electricity Tariff",
                //Expected Calculation model: base costs per month 5€ + consumption costs 22cent/kWh. To get annual cost need 5€ * 12 months.
                AnnualCosts = (5*12)+(consumption*0.22m) //
            };
        }
    }

}
