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
    public interface ITariffComparisionService
    {
        IEnumerable<TariffCost> GetComparedProducts(decimal consumption);
    }
    public class TariffComparisionService : ITariffComparisionService
    {
        private readonly ITariffComparisionFactory _factory;
        public TariffComparisionService(ITariffComparisionFactory factory)
        {
            _factory = factory;
        }
        public IEnumerable<TariffCost> GetComparedProducts(decimal consumption) { 
            List<TariffCost> tariffCosts = new();
            var _products =  _factory.GetAllTariffs(); 
            foreach (var product in _products)
            {
                tariffCosts.Add(new TariffCost { TariffName = product.Name, AnnualCosts = product.AnnualCostCalculation(consumption) });
            }

        return tariffCosts.OrderBy(tariff=>tariff.AnnualCosts);
        }
    }
}
