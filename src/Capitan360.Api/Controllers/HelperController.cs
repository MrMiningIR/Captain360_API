using Capitan360.Application.Common;
using Capitan360.Application.Services.Dtos;
using Capitan360.Application.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelperController(IIdentityService identityService) : ControllerBase
    {
        [HttpGet("userKinds")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<UserKindItemDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<UserKindItemDto>>), StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse<PagedResult<UserKindItemDto>>> GetUserKinds()
        {
            var userKinds = identityService.GetUserKindList();
            return Ok(userKinds);
        }

        [HttpGet("MoadianType")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<MoadianItemDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<MoadianItemDto>>), StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse<PagedResult<MoadianItemDto>>> GetMoadianTypes()
        {
            var userMoadian = identityService.GeMoadianList();
            return Ok(userMoadian);
        }

        [HttpGet("EntranceFeeType")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<EntranceFeeTypeDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<EntranceFeeTypeDto>>), StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse<PagedResult<EntranceFeeTypeDto>>> GetEntranceTypes()
        {
            var entranceFeeType = identityService.GetEntranceTypeList();
            return Ok(entranceFeeType);
        }

        [HttpGet("PathStructTypes")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<PathStructTypeDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<PathStructTypeDto>>), StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse<PagedResult<PathStructTypeDto>>> GetPathStructTypes()
        {
            var structTypes = identityService.GetPathStructTypeList();
            return Ok(structTypes);
        }

        [HttpGet("WeightTypes")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<WeightTypeDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<WeightTypeDto>>), StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse<PagedResult<WeightTypeDto>>> GetWeightTypes(List<int>? shouldRemove = null)
        {
            var weightTypes = identityService.GetWeightTypeList(shouldRemove);
            return Ok(weightTypes);
        }

        [HttpGet("Rates")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<RateDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<RateDto>>), StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse<PagedResult<RateDto>>> GetRates()
        {
            var rates = identityService.GetRateList();
            return Ok(rates);
        }

        [HttpGet("MiniCompanies/{companyTypeId}")]
        [ProducesResponseType(typeof(ApiResponse<List<CompanyItemDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<CompanyItemDto>>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<List<CompanyItemDto>>>> MiniCompanies(int companyTypeId, CancellationToken cancellationToken)
        {
            var response = await identityService.GetCompaniesList(companyTypeId, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }


    }
}