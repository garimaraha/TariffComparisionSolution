using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TariffComparisionModel.API.DTOs
{
    /// <summary>
    /// Response Tariff DTOs contains TariffName and AnnualCost for compared product.
    /// </summary>
    public record ResponseTariffDTO
    {
        // Property to hold the name of the tariff 
        public string TariffName { get; set; } = "";//Benefit: Ensures the tariff name is always initialized to an empty string, avoiding null values


        // Property to hold the annual costs for the tariff
        public decimal AnnualCosts { get; set; } // Benefit: Provides a precise representation of the annual cost in decimal format

    }
}
