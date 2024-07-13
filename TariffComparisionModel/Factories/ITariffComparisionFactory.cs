using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffComparisionModel.Products;

namespace TariffComparisionModel.Factories
{
    /// <summary>
    /// Factory interface defining the contract for creating different tariffs.
    /// Benefit: Provides a way to abstract and encapsulate the instantiation logic to get all avaiable tariffs, promoting loose coupling.

    /// </summary>
    public interface ITariffComparisionFactory
    {
        IEnumerable<ITariffProduct> GetAllTariffs();
    }
}
