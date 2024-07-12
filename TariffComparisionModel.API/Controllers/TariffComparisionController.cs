using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TariffComparisionModel.API.DTOs;
using TariffComparisionModel.API.Extension;
using TariffComparisionModel.Services;

namespace TariffComparisionModel.API.Controllers
{
    [Route("api/TariffComparision")]
    [ApiController]
    public class TariffComparisionController : ControllerBase
    {
        private readonly ITariffComparisionService _service;
        public TariffComparisionController(ITariffComparisionService service)
        {
            _service = service;
        }

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
                    return BadRequest(ModelState);
                
                if (consumptionReqDto.Consumption <= 0)
                    return BadRequest(StatusCodes.Status400BadRequest);

                IEnumerable<ResponseTariffDTO> tariffCosts = _service.GetComparedProducts(consumptionReqDto.Consumption).ConvertToDto();
                return Ok(tariffCosts.OrderBy(tariff => tariff.AnnualCosts));
            }
            catch (Exception) { 
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
