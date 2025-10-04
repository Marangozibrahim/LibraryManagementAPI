using Library.Application.Features.Categories.Commands.AddCategory;
using Library.Application.Features.Categories.Commands.DeleteCategory;
using Library.Application.Features.Categories.Commands.UpdateCategory;
using Library.Application.Features.Categories.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _mediator.Send(new GetCategoriesQuery());
        return Ok(result);
    }

    [HttpPost("add-category")]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpDelete("delete-category")]
    public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPut("update-category")]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }
}
