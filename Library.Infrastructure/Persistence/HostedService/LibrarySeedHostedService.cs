using Library.Application.Abstractions.Persistence;
using Library.Application.Common.Specifications;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Infrastructure.Persistence.HostedService
{
    //public sealed class LibrarySeedHostedService(IServiceProvider _sp) : IHostedService
    //{
    //    public async Task StartAsync(CancellationToken cancellationToken)
    //    {
    //        using var scope = _sp.CreateScope();
    //        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

    //        try
    //        {
    //            await uow.BeginTransactionAsync(cancellationToken);

    //            await SeedAuthorsAsync(uow, cancellationToken);
    //            await SeedCategoriesAsync(uow, cancellationToken);

    //            await uow.SaveChangesAsync(cancellationToken);

    //            await SeedBooksAsync(uow, cancellationToken);
    //            await SeedUsersAndBorrowsAsync(uow, scope.ServiceProvider, cancellationToken);

    //            await uow.SaveChangesAsync(cancellationToken);
    //            await uow.CommitTransactionAsync(cancellationToken);
    //        }
    //        catch
    //        {
    //            await uow.RollbackTransactionAsync(cancellationToken);
    //            throw;
    //        }
    //    }

    //    private async Task SeedAuthorsAsync(IUnitOfWork uow, CancellationToken cancellationToken)
    //    {
    //        var authorRepo = uow.Repository<Author>();
    //        var allAuthorsSpec = new AllSpecification<Author>();

    //        if ((await authorRepo.ListAsync(allAuthorsSpec, cancellationToken)).Count == 0)
    //        {
    //            var authors = new List<Author>
    //            {
    //                new Author { Name = "J.K. Rowling" },
    //                new Author { Name = "George R.R. Martin" },
    //                new Author { Name = "Agatha Christie" }
    //            };

    //            foreach (var author in authors)
    //                await authorRepo.AddAsync(author, cancellationToken);
    //        }
    //    }

    //    private async Task SeedCategoriesAsync(IUnitOfWork uow, CancellationToken cancellationToken)
    //    {
    //        var categoryRepo = uow.Repository<Category>();
    //        var allCategoriesSpec = new AllSpecification<Category>();

    //        if ((await categoryRepo.ListAsync(allCategoriesSpec, cancellationToken)).Count == 0)
    //        {
    //            var categories = new List<Category>
    //            {
    //                new Category { Name = "Fantasy" },
    //                new Category { Name = "Mystery" },
    //                new Category { Name = "Science Fiction" }
    //            };

    //            foreach (var category in categories)
    //                await categoryRepo.AddAsync(category, cancellationToken);
    //        }
    //    }

    //    private async Task SeedBooksAsync(IUnitOfWork uow, CancellationToken cancellationToken)
    //    {
    //        var bookRepo = uow.Repository<Book>();
    //        var authorRepo = uow.Repository<Author>();
    //        var categoryRepo = uow.Repository<Category>();

    //        var allBooksSpec = new AllSpecification<Book>();
    //        var allAuthorsSpec = new AllSpecification<Author>();
    //        var allCategoriesSpec = new AllSpecification<Category>();

    //        if ((await bookRepo.ListAsync(allBooksSpec, cancellationToken)).Count == 0)
    //        {
    //            var authorsList = await authorRepo.ListAsync(allAuthorsSpec, cancellationToken);
    //            var categoriesList = await categoryRepo.ListAsync(allCategoriesSpec, cancellationToken);

    //            var books = new List<Book>
    //            {
    //                new Book("Harry Potter and the Sorcerer's Stone", 10, authorsList[0].Id, categoriesList[0].Id),
    //                new Book("A Game of Thrones", 5, authorsList[1].Id, categoriesList[0].Id),
    //                new Book("Murder on the Orient Express", 7, authorsList[2].Id, categoriesList[1].Id)
    //            };

    //            foreach (var book in books)
    //                await bookRepo.AddAsync(book, cancellationToken);
    //        }
    //    }

    //    private async Task SeedUsersAndBorrowsAsync(IUnitOfWork uow, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    //    {
    //        var borrowRepo = uow.Repository<Borrow>();
    //        var bookRepo = uow.Repository<Book>();
    //        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

    //        var users = userManager.Users.ToList();
    //        if (!users.Any())
    //        {
    //            var defaultUser = new AppUser { UserName = "string", Email = "string" };
    //            await userManager.CreateAsync(defaultUser, "string");
    //            users.Add(defaultUser);
    //        }

    //        var allBorrowsSpec = new AllSpecification<Borrow>();
    //        if ((await borrowRepo.ListAsync(allBorrowsSpec, cancellationToken)).Count == 0)
    //        {
    //            var allBooksSpec = new AllSpecification<Book>();
    //            var booksList = await bookRepo.ListAsync(allBooksSpec, cancellationToken);

    //            var borrows = new List<Borrow>
    //            {
    //                new Borrow(booksList[0], users[0].Id, DateTime.UtcNow.AddDays(14)),
    //                new Borrow(booksList[1], users[0].Id, DateTime.UtcNow.AddDays(7))
    //            };

    //            foreach (var borrow in borrows)
    //                await borrowRepo.AddAsync(borrow, cancellationToken);
    //        }
    //    }

    //    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    //}
}