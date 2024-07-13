using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffComparisionModel.Products
{
    /// <summary>
    /// Concrete Product class for Packaged Tariff.
    /// Benefit: Encapsulates the specific calculation logic for the Packaged Tariff, adhering to the Single Responsibility Principle (SRP).
    /// </summary>
    public class PackagedTariffProduct : ITariffProduct
    {
        public string Name  {get;}
        private readonly decimal _basicAnnualCosnsumptionkWh;
        private readonly decimal _additionalRatePerUnit;
        private readonly decimal _fixedAnnualCost;
        /// <summary>
        /// /// <summary>
        /// class is immutable and values are provided when the class is instantiated.
        /// </summary>
        /// </summary>
        /// <param name="basicAnnualCosnsumptionkWh"></param>
        /// <param name="fixedAnnualCost"></param>
        /// <param name="additionalRatePerUnit"></param>
        /// <param name="tariffName"></param>
        public PackagedTariffProduct(decimal basicAnnualCosnsumptionkWh=4000,  decimal fixedAnnualCost=800, decimal additionalRatePerUnit=0.30m,string tariffName= "Packaged Tariff")
        {
             _basicAnnualCosnsumptionkWh = basicAnnualCosnsumptionkWh;
            _fixedAnnualCost = fixedAnnualCost;
            _additionalRatePerUnit = additionalRatePerUnit;
            Name = tariffName;
        }
        /// <summary>
        /// Calculates Annual Cost for Basic Electricity Tariff. 
        /// Calculation model: {_fixedAnnualCost}€ for up to  {_basicAnnualCosnsumptionkWh}kWh/year and above {_basicAnnualCosnsumptionkWh}kWh/year additionally {_additionalRatePerUnit} cent/kWh.
        /// </summary>
        /// <param name="usage"></param>
        /// <returns></returns>
        public decimal AnnualCostCalculation(decimal consumption)
        {
            if(consumption <=  _basicAnnualCosnsumptionkWh)
            {
                return _fixedAnnualCost;
            }
            if (consumption < 0)
                throw new ArgumentException("Consumption must not be negative");

            var additionalConsumption = (consumption - _basicAnnualCosnsumptionkWh);
            var additionalConsumptionCost = additionalConsumption * _additionalRatePerUnit;
            return _fixedAnnualCost + additionalConsumptionCost;
        }
    }
}
