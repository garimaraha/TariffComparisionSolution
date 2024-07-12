using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffComparisionModel.Products
{
    public class PackagedTariffProduct : ITariffProduct
    {
        public string Name  {get;}
        private readonly decimal _basicAnnualCosnsumptionkWh;
        private readonly decimal _additionalRatePerUnit;
        private readonly decimal _fixedAnnualCost;
        
       public PackagedTariffProduct(decimal basicAnnualCosnsumptionkWh=4000,  decimal fixedAnnualCost=800, decimal additionalRatePerUnit=0.30m,string tariffName= "Packaged Tariff")
        {
             _basicAnnualCosnsumptionkWh = basicAnnualCosnsumptionkWh;
            _fixedAnnualCost = fixedAnnualCost;
            _additionalRatePerUnit = additionalRatePerUnit;
            Name = tariffName;
        }
        public decimal AnnualCostCalculation(decimal usage)
        {
            if(usage <=  _basicAnnualCosnsumptionkWh)
            {
                return _fixedAnnualCost;
            }
            var additionalConsumption = (usage - _basicAnnualCosnsumptionkWh);
            var additionalConsumptionCost = additionalConsumption * _additionalRatePerUnit;
            return _fixedAnnualCost + additionalConsumptionCost;
        }
    }
}
