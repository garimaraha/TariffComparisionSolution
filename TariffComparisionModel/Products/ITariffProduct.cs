using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffComparisionModel.Products
{
    public interface ITariffProduct
    {
        string Name { get; }
        decimal AnnualCostCalculation(decimal usage);
    }
}
