using Library.Application.Dtos.Book;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace Library.IntegrationTests;

public class BooksIntegrationTests : IntegrationTestBase
{
    public BooksIntegrationTests(IntegrationTestFactory factory) : base(factory) { }

    [Fact]
    public async Task GetBooks_ShouldReturnSeededBooks()
    {
        // Act
        var response = await Client.GetAsync("/api/books");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var books = await response.Content.ReadFromJsonAsync<List<BookDto>>();
        books.Should().NotBeNull();
        books.Should().Contain(b => b.Title == "Harry Potter");
        books.First().TotalCopies.Should().BeGreaterThanOrEqualTo(0);
    }
}
