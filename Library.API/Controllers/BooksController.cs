using Library.Application.Dtos.Book;
using Library.Application.Features.Books.Commands.AddBook;
using Library.Application.Features.Books.Commands.DeleteBook;
using Library.Application.Features.Books.Commands.UpdateBook;
using Library.Application.Features.Books.Queries.GetBookById;
using Library.Application.Features.Books.Queries.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetFilteredResultsAsync([FromQuery] GetBooksQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync([FromBody] AddBookCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(Guid id, [FromBody] UpdateBookRequest request)
        {
            var command = new UpdateBookCommand(
                id,
                request.Title,
                request.TotalCopies,
                request.CopiesAvailable,
                request.AuthorId,
                request.CategoryId
            );

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(Guid id)
        {
            await _mediator.Send(new DeleteBookCommand(id));
            return NoContent();
        }
    }
}
