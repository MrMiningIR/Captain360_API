using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.ContentTypeService.Commands.CreateContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.DeleteContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.MoveDownContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.MoveUpContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.UpdateActiveStateContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.UpdateContentType;
using Capitan360.Application.Services.ContentTypeService.Dtos;
using Capitan360.Application.Services.ContentTypeService.Queries.GetAllContentTypes;
using Capitan360.Application.Services.ContentTypeService.Queries.GetContentTypeById;
using Capitan360.Application.Services.ContentTypeService.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/ContentTypes")]
[ApiController]
[PermissionFilter("بخش محتوی", "Q")]
public class ContentTypesController(IContentTypeService contentTypeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<ContentTypeDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<ContentTypeDto>>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("لیست محتوی", "Q1")]
    public async Task<ActionResult<ApiResponse<PagedResult<ContentTypeDto>>>> GetAllContentTypes(
        [FromQuery] GetAllContentTypesQuery query, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.GetAllContentTypes(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
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

    [HttpPost]
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
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [PermissionFilter("حذف محتوی", "Q4")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteContentType(
        [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.DeleteContentTypeAsync(new DeleteContentTypeCommand(id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
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

    [HttpPost("MoveUpContent")]
    [PermissionFilter("تغییر ترتیب - بالا", "Q6")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> MoveUpContentType(MoveContentTypeUpCommand moveContentTypeUpCommand, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.MoveContentTypeUpAsync(moveContentTypeUpCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("MoveDownContent")]
    [PermissionFilter("تغییر ترتیب - پایین", "Q7")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> MoveDownContentType(MoveContentTypeDownCommand moveContentTypeDownCommand, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.MoveContentTypeDownAsync(moveContentTypeDownCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("ChangeContentTypeActiveStatus")]
    [PermissionFilter("تغییر وضعیت محتوی ", "Q8")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<int>>> ChangeContentTypeActiveStatus([FromBody] UpdateActiveStateContentTypeCommand command, CancellationToken cancellationToken)
    {
        var response = await contentTypeService.SetContentActivityStatus(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}