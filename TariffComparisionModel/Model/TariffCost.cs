
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
