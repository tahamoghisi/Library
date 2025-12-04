using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Context
{
    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext(DbContextOptions<LibraryDBContext> options)
        : base(options){ }


        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Books)
                .HasForeignKey(p => p.CategoryId);

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Loans)
            //    .WithOne(u => u.User);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Books)
                .WithOne(c => c.Category);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)
                .WithMany(l => l.Loans)
                .HasForeignKey(l => l.BookId);



            modelBuilder.Entity<Loan>()
                .HasOne(l => l.User)
                .WithMany(l => l.Loans)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Role>()
                .HasData(
                new Role { Id = 1, RoleName = "Admin" },
                new Role { Id = 2, RoleName = "Member" }
                );
        }
    }
}
