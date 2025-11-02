using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.ContentTypes.Commands.Create;
using Capitan360.Application.Features.ContentTypes.Commands.Delete;
using Capitan360.Application.Features.ContentTypes.Commands.MoveDown;
using Capitan360.Application.Features.ContentTypes.Commands.MoveUp;
using Capitan360.Application.Features.ContentTypes.Commands.Update;
using Capitan360.Application.Features.ContentTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.ContentTypes.Dtos;
using Capitan360.Application.Features.ContentTypes.Queries.GetAll;
using Capitan360.Application.Features.ContentTypes.Queries.GetById;
using Capitan360.Application.Features.ContentTypes.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/ContentTypes")]
[ApiController]
[PermissionFilter("بخش محتوی", "Q")]
public class ContentTypesController(IContentTypeService contentTypeService) : ControllerBase
{
    [HttpGet("GetAllContentTypes")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<ContentTypeDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<ContentTypeDto>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<ContentTypeDto>>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<ContentTypeDto>>), StatusCodes.Status401Unauthorized)]
    [PermissionFilter("لیست محتوی", "Q1")]
    public async Task<ActionResult<ApiResponse<PagedResult<ContentTypeDto>>>> GetAllContentTypes(
        [FromQuery] GetAllContentTypesQuery query, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.GetAllContentTypesAsync(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetContentTypeById/{id}")]
    [ProducesResponseType(typeof(ApiResponse<ContentTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ContentTypeDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ContentTypeDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("دریافت محتوی", "Q2")]
    public async Task<ActionResult<ApiResponse<ContentTypeDto>>> GetContentTypeById(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.GetContentTypeByIdAsync(new GetContentTypeByIdQuery(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("CreateContentType")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("ساخت محتویی جدید", "Q3")]
    public async Task<ActionResult<ApiResponse<int>>> CreateContentType(
        [FromBody] CreateContentTypeCommand command, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.CreateContentTypeAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    [PermissionFilter("حذف محتوی", "Q4")]
    public async Task<ActionResult<ApiResponse<int>>> DeleteContentType(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.DeleteContentTypeAsync(new DeleteContentTypeCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateContentType/{id}")]
    [ProducesResponseType(typeof(ApiResponse<ContentTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ContentTypeDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ContentTypeDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("آپدیت محتوی", "Q5")]
    public async Task<ActionResult<ApiResponse<ContentTypeDto>>> UpdateContentType([FromRoute] int id,
        [FromBody] UpdateContentTypeCommand updateContentTypeCommand, CancellationToken cancellationToken)
    {
        updateContentTypeCommand.Id = id;

        var response = await contentTypeService.UpdateContentTypeAsync(updateContentTypeCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveUpContentType")]
    [PermissionFilter("تغییر ترتیب - بالا", "Q6")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<int>>> MoveUpContentType(MoveUpContentTypeCommand moveContentTypeUpCommand, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.MoveUpContentTypeAsync(moveContentTypeUpCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveDownContentType")]
    [PermissionFilter("تغییر ترتیب - پایین", "Q7")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<int>>> MoveDownContentType(MoveDownContentTypeCommand moveContentTypeDownCommand, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.MoveDownContentTypeAsync(moveContentTypeDownCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("ChangeContentTypeActiveStatus")]
    [PermissionFilter("تغییر وضعیت محتوی ", "Q8")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<int>>> ChangeContentTypeActiveStatus([FromBody] UpdateActiveStateContentTypeCommand command, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.SetContentTypeActivityStatusAsync(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}