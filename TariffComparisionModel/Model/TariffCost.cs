using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffComparisionModel.Model
{
    /// <summary>
    /// Domain object will return the Tariff Name and Annual Cost.
    /// Benefit: Used decimal over double, as this is a financial calculation.
    /// </summary>
    public class TariffCost
    {
        public string TariffName { get; set; } = "";
        public decimal AnnualCosts {  get; set; }
    }
}
