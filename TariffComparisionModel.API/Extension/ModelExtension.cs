using TariffComparisionModel.API.DTOs;
using TariffComparisionModel.Model;
using TariffComparisionModel.Products;

namespace TariffComparisionModel.API.Extension
{
    public static class ModelExtensions
    {
        public static ResponseTariffDTO ConvertToDto(this TariffCost tariff)
        {
            return new ResponseTariffDTO
            {
                TariffName = tariff.TariffName,
                AnnualCosts = tariff.AnnualCosts.FormatDecimal() 
            };
        }

        public static IEnumerable<ResponseTariffDTO> ConvertToDto(this IEnumerable<TariffCost> tariffs)
        {
            return tariffs.Select(product => product.ConvertToDto());
        }
        public static decimal FormatDecimal(this decimal value)
        {
            //Check is the value is an integer - then remove precision by truncating the decimal value
            if (value % 1 == 0) { return Math.Truncate(value); }
            return Math.Round(value, 2);
        }
    }
}
