using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffComparisionModel.Factories;
using TariffComparisionModel.Model;
using TariffComparisionModel.Products;

namespace TariffComparisionModel.Services
{
    /// <summary>
    /// Created Interface for Unit Test Mocking
    /// </summary>
    public interface ITariffComparisionService
    {
        IEnumerable<TariffCost> GetComparedProducts(decimal consumption);
    }
    /// <summary>
    /// This class is responsible solely for returning the compared tariff details, including the tariff name and annual cost,
    /// thereby adhering to the Single Responsibility Principle (SRP)
    /// </summary>
    public class TariffComparisionService : ITariffComparisionService
    {
        private readonly ITariffComparisionFactory _factory;
        public TariffComparisionService(ITariffComparisionFactory factory)
        {
            _factory = factory; //Inject Factory to get all available tariffs.
        }
        /// <summary>
        /// 
        /// Benefit:No modifications will be required in the future to accommodate additional products, ensuring compliance with the Open/Closed Principle (OCP)
        /// </summary>
        /// <param name="consumption"></param>
        /// <returns></returns>
        public IEnumerable<TariffCost> GetComparedProducts(decimal consumption) {
            try
            {
                // Create a list to hold the calculated tariff costs
                List<TariffCost> tariffCosts = new();

                // Use the factory to get all available products/tariffs
                var _products = _factory.GetAllTariffs(); 
                foreach (var product in _products)
                {
                    // Calculate the annual cost for the given consumption and add it to the list
                    tariffCosts.Add(new TariffCost { TariffName = product.Name, AnnualCosts = product.AnnualCostCalculation(consumption) });
                }

                return tariffCosts.OrderBy(tariff => tariff.AnnualCosts);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
