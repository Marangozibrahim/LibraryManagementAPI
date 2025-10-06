using Library.Application.Dtos.Author;
using Library.Application.Features.Authors.Commands.AddAuthor;
using Library.Application.Features.Authors.Commands.DeleteAuthor;
using Library.Application.Features.Authors.Commands.UpdateAuthor;
using Library.Application.Features.Authors.Queries.GetAuthorById;
using Library.Application.Features.Authors.Queries.GetAuthors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAuthorsAsync()
        {
            var result = await _mediator.Send(new GetAuthorsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetAuthorByIdQuery(id));
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddAuthorAsync([FromBody] AddAuthorCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthorAsync(Guid id, [FromBody] UpdateAuthorRequest request)
        {
            var command = new UpdateAuthorCommand(id, request.Name);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorAsync(Guid id)
        {
            await _mediator.Send(new DeleteAuthorCommand(id));
            return NoContent();
        }
    }
}
