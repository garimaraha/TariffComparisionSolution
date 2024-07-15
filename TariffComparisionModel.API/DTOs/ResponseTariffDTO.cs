using System.Text.Json.Serialization;

namespace TariffComparisionModel.API.DTOs
{
    /// <summary>
    /// Response Tariff DTOs contains TariffName and AnnualCost for compared product.
    /// </summary>
    public record ResponseTariffDTO
    {
        // Property to hold the name of the tariff 
        [JsonPropertyName("Tariff name")]
        public string TariffName { get; set; } = "";//Benefit: Ensures the tariff name is always initialized to an empty string, avoiding null values


        // Property to hold the annual costs for the tariff
        [JsonPropertyName("Annual costs (€/year)")]
        public decimal AnnualCosts { get; set; } // Benefit: Provides a precise representation of the annual cost in decimal format

    }
}
