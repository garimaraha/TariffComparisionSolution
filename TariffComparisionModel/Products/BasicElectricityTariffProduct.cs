using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffComparisionModel.Products
{
    public class BasicElectricityTariffProduct : ITariffProduct
    {
        public string Name { get; }
        private readonly decimal _baseCostsPerMonth;
        private readonly decimal _ratePerUnit;


        public BasicElectricityTariffProduct(decimal baseCostsPerMonth=5, decimal ratePerUnit=0.22m,string tariffName = "Basic Electricity Tariff")
        {
            _baseCostsPerMonth = baseCostsPerMonth;
            _ratePerUnit = ratePerUnit;
            Name = tariffName;
        }

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
