using Library.Application.Features.Books.Commands.DeleteBook;
using Library.Application.Features.Borrows.Commands.BorrowBook;
using Library.Application.Features.Borrows.Commands.DeleteBorrow;
using Library.Application.Features.Borrows.Commands.ReturnBook;
using Library.Application.Features.Borrows.Queries.GetBorrowById;
using Library.Application.Features.Borrows.Queries.GetBorrowsByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/borrows")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class BorrowsController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBorrowsByUser()
        {
            var result = await _mediator.Send(new GetBorrowsByUserQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBorrowByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetBorrowByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBookAsync([FromBody] BorrowBookCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnBookAsync(Guid id)
        {
            await _mediator.Send(new ReturnBookCommand(id));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBorrowAsync(Guid id)
        {
            await _mediator.Send(new DeleteBorrowCommand(id));
            return NoContent();
        }
    }
}
