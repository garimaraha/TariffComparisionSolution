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
                AnnualCosts = Math.Round(tariff.AnnualCosts, 2)
            };
        }

        public static IEnumerable<ResponseTariffDTO> ConvertToDto(this IEnumerable<TariffCost> tariffs)
        {
            return tariffs.Select(product => product.ConvertToDto());
        }
    }
}
