using TariffComparisionModel.API.DTOs;
using TariffComparisionModel.Model;

namespace TariffComparisionModel.API.Extension
{
    /// <summary>
    /// Extension methods to help map domain objects to DTO objects.
    /// Benefit: Promotes code reusability and separation of concerns by centralizing conversion logic.
    /// </summary>
    public static class ModelExtensions
    {
        /// <summary>
        ///  Converts TariifCost domian object  to  ResponseTariff DTO object.
        /// </summary>
        /// <param name="tariff"></param>
        /// <returns>A ResponseTariffDTO instance with mapped properties.</returns>
        public static ResponseTariffDTO ConvertToDto(this TariffCost tariff)
        {
            return new ResponseTariffDTO
            {
                TariffName = tariff.TariffName,
                AnnualCosts = tariff.AnnualCosts.FormatDecimal() // Formats AnnualCosts to show the value to two decimal places using the FormatDecimal extension method.
            };
        }
        /// <summary>
        /// Converts a list of TariffCost domain objects to a list of ResponseTariff DTO objects.
        /// Benefit: Enables bulk conversion of domain objects to DTOs for easier handling in responses.
        /// </summary>
        /// <param name="tariffs"></param>
        /// <returns></returns>
        public static IEnumerable<ResponseTariffDTO> ConvertToDto(this IEnumerable<TariffCost> tariffs)
        {
            return tariffs.Select(product => product.ConvertToDto()); // Maps each TariffCost to ResponseTariffDTO.
        }
        /// <summary>
        /// Formats a decimal value for improved readability.
        /// Benefit: Ensures consistent presentation of monetary values in the application.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal FormatDecimal(this decimal value)
        {
            //Check is the value is an integer - then remove precision by truncating the decimal value
            if (value % 1 == 0) { return Math.Truncate(value); }
            return Math.Round(value, 2);//The decimal value rounded to two decimal places.
        }
    }
}
