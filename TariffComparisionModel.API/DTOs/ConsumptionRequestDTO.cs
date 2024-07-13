using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace TariffComparisionModel.API.DTOs
{
    /// <summary>
    /// Consumption should accept always 0 and positive numbers.
    /// </summary>
    public class ConsumptionRequestDTO
    {
        [BindRequired] // Ensures that the Consumption property is part of the request query parameters.
        public decimal Consumption {  get; set; } // Property to hold the consumption value in kilowatt-hours per year (kWh/year)

    }
}
