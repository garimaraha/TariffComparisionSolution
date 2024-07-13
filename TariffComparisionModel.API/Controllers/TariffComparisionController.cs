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
        public ActionResult<IEnumerable<ResponseTariffDTO>> GetTariffComparisons([FromQuery] ConsumptionRequestDTO consumptionReqDto)
        {
            try
            {
                if (!ModelState.IsValid)               
                    return BadRequest(ModelState); // Return BadRequest for any data conversion issue in the query parameter.

                if (consumptionReqDto.Consumption < 0)
                    return BadRequest(); // Return BadRequest if the consumption value is negative.


                IEnumerable<ResponseTariffDTO> tariffCosts = _service.GetComparedProducts(consumptionReqDto.Consumption).ConvertToDto(); //ConvertToDTo() is an etxtension method to convert domain object to DTO object.
                return Ok(tariffCosts.OrderBy(tariff => tariff.AnnualCosts));// Return tariff costs ordered by annual costs in ascending order.
            }
            catch (Exception) {

                // Handle any unexpected exceptions by returning a 500 Internal Server Error status code
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
