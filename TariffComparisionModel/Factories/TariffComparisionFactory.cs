using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffComparisionModel.Products;

namespace TariffComparisionModel.Factories
{
    /// <summary>
    /// Concrete factory for returing avaiable tariff instances by injecting tariff configuration values .
    /// Benefit: Encapsulates the creation logic for getting and instantiating different tariffs , making the codebase more maintainable and adhering to the Open/Closed Principle (OCP).
    /// </summary>
    public class TariffComparisionFactory:ITariffComparisionFactory
    {
        private readonly IConfiguration _configuration;
        public TariffComparisionFactory()
        {
            // Build configuration
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("TariffConfig.json", optional: false, reloadOnChange: true) //Binding Json to reterive tariif configurations.
                .Build();
        }
        /// <summary>
        /// Return all available tariff instances by providing configuration values.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ITariffProduct> GetAllTariffs()
            {
               return new List<ITariffProduct>
               {
                  
                 new BasicElectricityTariffProduct(
                _configuration.GetValue<decimal>("ProductTariffs:BasicElectricity:MonthlyFixedRate"),
                _configuration.GetValue<decimal>("ProductTariffs:BasicElectricity:RatePerUnit"),
                _configuration.GetValue<string>("ProductTariffs:BasicElectricity:TariffName")??""
            ),
                  new PackagedTariffProduct(
                    _configuration.GetValue<decimal>("ProductTariffs:PackagedTariff:BasicAnnualUsage"),
                    _configuration.GetValue<decimal>("ProductTariffs:PackagedTariff:FixedAnnualCost"),
                    _configuration.GetValue<decimal>("ProductTariffs:PackagedTariff:AdditionalRatePerUnit"),
                    _configuration.GetValue<string>("ProductTariffs:PackagedTariff:TariffName")??""
                )
               };
            }
        }
    
}
