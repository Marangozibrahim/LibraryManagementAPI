using Library.Application.Dtos.Category;
using Library.Application.Features.Categories.Commands.AddCategory;
using Library.Application.Features.Categories.Commands.DeleteCategory;
using Library.Application.Features.Categories.Commands.UpdateCategory;
using Library.Application.Features.Categories.Queries.GetCategories;
using Library.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;
[Route("api/categories")]
[ApiController]
public class CategoriesController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _mediator.Send(new GetCategoriesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryByIdAsync(Guid id)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id));
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var command = new UpdateCategoryCommand(id, request.Name);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _mediator.Send(new DeleteCategoryCommand(id));
        return NoContent();
    }
}
