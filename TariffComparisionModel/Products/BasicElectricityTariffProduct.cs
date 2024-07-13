using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffComparisionModel.Products
{
    /// <summary>
    /// Concrete Product class for Basic Tariff.
    /// Benefit: Encapsulates the specific calculation logic for the Basic Tariff, adhering to the Single Responsibility Principle (SRP).
    /// </summary>
    public class BasicElectricityTariffProduct : ITariffProduct
    {
        public string Name { get; }
        private readonly decimal _baseCostsPerMonth;
        private readonly decimal _ratePerUnit;

        /// <summary>
        /// class is immutable and values are provided when the class is instantiated.
        /// </summary>
        /// <param name="baseCostsPerMonth"></param>
        /// <param name="ratePerUnit"></param>
        /// <param name="tariffName"></param>
        public BasicElectricityTariffProduct(decimal baseCostsPerMonth=5, decimal ratePerUnit=0.22m,string tariffName = "Basic Electricity Tariff")
        {
            _baseCostsPerMonth = baseCostsPerMonth;
            _ratePerUnit = ratePerUnit;
            Name = tariffName;
        }
        /// <summary>
        /// Calculates Annual Cost for Basic Electricity Tariff. 
        /// Calculation model: base costs per month {baseCostsPerMonth}€ + consumption costs {ratePerUnit}cent/kWh
        /// </summary>
        /// <param name="consumption"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public decimal AnnualCostCalculation(decimal consumption)
        {
           decimal annualBaseCosts = _baseCostsPerMonth * 12;
            if (consumption == 0) { return annualBaseCosts; }
            if (consumption < 0)
                throw new ArgumentException("Consumption must not be negative");

            decimal additionalConsumptionCosts = _ratePerUnit * consumption;
            return annualBaseCosts + additionalConsumptionCosts;
        }
    }
}
