using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TariffComparisionModel.API.DTOs;
using TariffComparisionModel.API.Extension;
using TariffComparisionModel.Services;

namespace TariffComparisionModel.API.Controllers
{
    [Route("api/TariffComparision")] //Base Route
    [ApiController]
    public class TariffComparisionController : ControllerBase
    {
        private readonly ITariffComparisionService _service;
        public TariffComparisionController(ITariffComparisionService service)
        {
            _service = service; //Added Dependecy of Tariff Comparision service and this is the entry point to access Tariff Comparision Model.
        }
        /// <summary>
        /// This Controller API has single responsibility to communicate with Tariff Comaprision Service Model to get compared product
        /// This API will convert Tariif Cost Model to Response Tariff DTO for better maintainability and returned Tariffs in ascending order by Annual Costs
        /// </summary>
        /// <param name="consumptionReqDto"></param>
        /// <returns></returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("compareCosts")]
        [HttpGet]
        public async Task<IActionResult> GetTariffComparisons([FromQuery] ConsumptionRequestDTO consumptionReqDto)
        {

            if (consumptionReqDto == null) // Check if the input model is null
            {
                throw new ArgumentNullException(nameof(consumptionReqDto), "Input model cannot be null."); // Throw an exception if null
            }
            if (consumptionReqDto.Consumption < 0)
                throw new ArgumentException("Consumption (kWh/year) value must be zero or a positive number.", nameof(consumptionReqDto)); // Return BadRequest if the consumption value is negative.


            IEnumerable<ResponseTariffDTO> tariffCosts = await Task.FromResult(_service.GetComparedProducts(consumptionReqDto.Consumption).ConvertToDto()); //ConvertToDTo() is an etxtension method to convert domain object to DTO object.
            return Ok(tariffCosts.OrderBy(tariff => tariff.AnnualCosts));// Return tariff costs ordered by annual costs in ascending order.

        }
    }
}
