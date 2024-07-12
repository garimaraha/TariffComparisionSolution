using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TariffComparisionModel.API.DTOs
{
    public class ResponseTariffDTO
    {
           
        public string TariffName { get; set; } = "";

        public decimal AnnualCosts { get; set; }
       
    }
}
