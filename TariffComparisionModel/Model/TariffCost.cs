using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffComparisionModel.Model
{
    public class TariffCost
    {
        public string TariffName { get; set; } = "";
        public decimal AnnualCosts {  get; set; }
    }
}
