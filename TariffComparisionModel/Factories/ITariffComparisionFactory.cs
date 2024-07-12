using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffComparisionModel.Products;

namespace TariffComparisionModel.Factories
{
    public interface ITariffComparisionFactory
    {
        IEnumerable<ITariffProduct> GetAllTariffs();
    }
}
