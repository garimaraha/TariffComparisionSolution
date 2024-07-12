using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace TariffComparisionModel.API.DTOs
{
    public class ConsumptionRequestDTO
    {
        [BindRequired]
        public decimal Consumption {  get; set; }
       
    }
}
