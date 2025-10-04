using Library.Application.Features.Authors.Commands.AddAuthor;
using Library.Application.Features.Authors.Commands.DeleteAuthor;
using Library.Application.Features.Authors.Commands.UpdateAuthor;
using Library.Application.Features.Authors.Queries.GetAuthors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAuthorsAsync()
        {
            var result = await _mediator.Send(new GetAuthorsQuery());
            return Ok(result);
        }

        [HttpPost("add-author")]
        public async Task<IActionResult> AddAuthorAsync([FromBody] AddAuthorCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("delete-author")]
        public async Task<IActionResult> DeleteAuthorAsync([FromBody] DeleteAuthorCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("update-author")]
        public async Task<IActionResult> UpdateAuthorAsync([FromBody] UpdateAuthorCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
