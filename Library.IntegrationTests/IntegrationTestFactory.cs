using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Library.IntegrationTests;

public class IntegrationTestFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.RemoveAll<AppDbContext>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();

            SeedData(db);
        });
    }

    private void SeedData(AppDbContext db)
    {
        var author = new Author() { Name = "J.K. Rowling" };
        var category = new Category() { Name = "Fantasy" };
        var book = new Book("Harry Potter", 4, author.Id, category.Id);

        db.Authors.Add(author);
        db.Categories.Add(category); 
        db.SaveChanges();
        db.Books.Add(book);
        db.SaveChanges();
    }
}
