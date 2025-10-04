using Library.Domain.Entities;
using Library.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence
{
    public class AppUser : IdentityUser<Guid>;
    public class AppRole : IdentityRole<Guid>;
    public class AppUserRole : IdentityUserRole<Guid>;

    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
         IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Borrow>()
                .HasOne<AppUser>()              
                .WithMany()                     
                .HasForeignKey(bw => bw.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Borrow>()
                .HasOne(bw => bw.Book)
                .WithMany(bk => bk.Borrows)
                .HasForeignKey(bw => bw.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Author>()
                .HasIndex(a => a.Name)
                .IsUnique();

            builder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.Entity<Book>()
                .HasIndex(b => new { b.Title, b.AuthorId })
                .IsUnique();

            #region Identity configuration
            builder.Entity<AppUserRole>().HasKey(x => new { x.UserId, x.RoleId });

            builder.Ignore<IdentityUserClaim<Guid>>();
            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            #endregion
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DateTime utcNow = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = utcNow;
                    entry.Entity.UpdatedAt = utcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = utcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
