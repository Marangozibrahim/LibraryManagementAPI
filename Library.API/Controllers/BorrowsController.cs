using Library.Application.Features.Borrows.Commands.BorrowBook;
using Library.Application.Features.Borrows.Commands.ReturnBook;
using Library.Application.Features.Borrows.Queries.GetBorrowsByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class BorrowsController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("my")]
        public async Task<IActionResult> GetBorrowsByUser()
        {
            var result = await _mediator.Send(new GetBorrowsByUserQuery());
            return Ok(result);
        }

        [HttpPost("borrow-book")]
        public async Task<IActionResult> BorrowBookAsync([FromBody] BorrowBookCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("return-book")]
        public async Task<IActionResult> ReturnBookAsync([FromBody] ReturnBookCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
