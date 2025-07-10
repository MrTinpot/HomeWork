using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shop.Core.Entities;

namespace Shop.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Orders> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=ShopData; Trusted_Connection=True; TrustServerCertificate=True;");

            optionsBuilder.ConfigureWarnings(warnings =>
        warnings.Ignore(RelationalEventId.PendingModelChangesWarning)); // db oluşturma hatası için

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                CreateDate = DateTime.Now,
                Id = 1,
                Email = "admin@admin.com",
                IsActive = true,
                IsAdmin = true,
                Name = "admin",
                Surname = "test",
                UserName = "adminuser",
                Password = "admin"
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
