using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffComparisionModel.Products
{
    /// <summary>
    /// Product interface defining the contract for tariff calculations.
    /// Benefit: Defines a common interface for all tariff products, ensuring consistency and interchangeability.
    /// </summary>
    public interface ITariffProduct
    {
        string Name { get; }
        decimal AnnualCostCalculation(decimal consumption);
    }
}
