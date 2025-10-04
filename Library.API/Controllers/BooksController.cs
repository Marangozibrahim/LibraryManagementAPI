using Library.Application.Features.Books.Commands.AddBook;
using Library.Application.Features.Books.Commands.DeleteBook;
using Library.Application.Features.Books.Commands.UpdateBook;
using Library.Application.Features.Books.Queries.GetAllBooks;
using Library.Application.Features.Books.Queries.GetBookById;
using Library.Application.Features.Books.Queries.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IMediator _mediator) : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var result = await _mediator.Send(new GetAllBooksQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredResultsAsync([FromQuery] GetBooksQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("add-book")]
        public async Task<IActionResult> AddBookAsync([FromBody] AddBookCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("delete-book")]
        public async Task<IActionResult> DeleteBookAsync([FromBody] DeleteBookCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("update-book")]
        public async Task<IActionResult> UpdateBookAsync([FromBody] UpdateBookCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
