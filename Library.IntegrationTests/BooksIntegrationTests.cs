using FluentAssertions;
using Library.Application.Common;
using Library.Domain.Entities;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

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

        var books = await response.Content.ReadFromJsonAsync<PaginatedResult<Book>>(
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        books.Should().NotBeNull();
        books.Items.Should().Contain(b => b.Title == "Harry Potter");
        books.Items.First().TotalCopies.Should().BeGreaterThanOrEqualTo(0);
    }
}
