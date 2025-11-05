using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyBanks.Dtos;
using Capitan360.Application.Features.Companies.CompanyBanks.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyBanks.Queries.GetById;
using Capitan360.Application.Features.Companies.CompanyBanks.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/CompanyBankC")]
    [ApiController]
    [PermissionFilter("بخش بانک", "V")]

    public class CompanyBankController(ICompanyBankService companyBankService) : ControllerBase
    {
        [HttpGet("GetAllCompanyBanks")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyBankDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyBankDto>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyBankDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<CompanyBankDto>>), StatusCodes.Status401Unauthorized)]
        [PermissionFilter(displayName: "لیست بانک ها", "V1")]
        public async Task<ActionResult<ApiResponse<PagedResult<CompanyBankDto>>>> GetAllCompanyBanks(
    [FromQuery] GetAllCompanyBanksQuery query, CancellationToken cancellationToken)
        {
            var response = await companyBankService.GetAllCompanyBanksAsync(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetCompanyBankById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CompanyBankDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CompanyBankDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CompanyBankDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<CompanyBankDto>), StatusCodes.Status403Forbidden)]
        [PermissionFilter(displayName: "دریافت بانک", "V2")]
        public async Task<ActionResult<ApiResponse<CompanyBankDto>>> GetCompanyBankById(
            [FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyBankService.GetCompanyBankByIdAsync(new GetCompanyBankByIdQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("CreateCompanyBank")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [PermissionFilter(displayName: "افزودن بانک", "V3")]
        public async Task<ActionResult<ApiResponse<int>>> CreateCompanyBank(
            [FromBody] CreateCompanyBankCommand command, CancellationToken cancellationToken)
        {
            var response = await companyBankService.CreateCompanyBankAsync(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
        [PermissionFilter(displayName: "حذف بانک", "V4")]
        public async Task<ActionResult<ApiResponse<int>>> DeleteCompanyBank(
            [FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await companyBankService.DeleteCompanyBankAsync(new DeleteCompanyBankCommand(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateCompanyBank/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CompanyBankDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CompanyBankDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CompanyBankDto>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<CompanyBankDto>), StatusCodes.Status401Unauthorized)]
        [PermissionFilter(displayName: "آپدیت بانک", "V5")]

        public async Task<ActionResult<ApiResponse<CompanyBankDto>>> UpdateCompanyBank([FromRoute] int id,
            [FromBody] UpdateCompanyBankCommand updateCompanyBankCommand, CancellationToken cancellationToken)
        {
            updateCompanyBankCommand.Id = id;

            var response = await companyBankService.UpdateCompanyBankAsync(updateCompanyBankCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("MoveUpCompanyBank")]
        [PermissionFilter("تغییر ترتیب - بالا", "V6")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<int>>> MoveUpCompanyBank(MoveUpCompanyBankCommand moveUpCompanyBankCommand, CancellationToken cancellationToken)
        {
            var response = await companyBankService.MoveUpCompanyBankAsync(moveUpCompanyBankCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("MoveDownCompanyBank")]
        [PermissionFilter("تغییر ترتیب - بالا", "V7")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<int>>> MoveDownCompanyBank(MoveDownCompanyBankCommand moveDownCompanyBankCommand, CancellationToken cancellationToken)
        {
            var response = await companyBankService.MoveDownCompanyBankAsync(moveDownCompanyBankCommand, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("ChangeCompanyBankActiveStatus")]
        [PermissionFilter("تغییر وضعیت بانک", "V8")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<int>>> ChangeCompanyBankActiveStatus([FromBody] UpdateActiveStateCompanyBankCommand command, CancellationToken cancellationToken)
        {
            var response = await companyBankService.SetCompanyBankActivityStatusAsync(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
