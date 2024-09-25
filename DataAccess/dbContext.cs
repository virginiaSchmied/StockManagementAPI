using Microsoft.EntityFrameworkCore;
using StockManagementAPI.Models.Product;
using StockManagementAPI.Models.User;
using System.Data;

namespace StockManagementAPI.DataAccess
{
    public class stockManagementDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        //Constructor
        public stockManagementDbContext(DbContextOptions<stockManagementDbContext> options, IConfiguration configuration) : base(options)
        {
        }

        // Entities
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductsCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategories");
            modelBuilder.Entity<Product>().ToTable("Products");

            // Seed Role
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, Name = "Admin", Description = "User role with authorized permissions" },
                new UserRole { Id = 2, Name = "Regular", Description = "User role with limited permissions" }
            );

            // Seed User
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "schmiedvirginia@gmail.com", Password = encryptPass.GetSHA256("Virginia2024"), Role_Id = 1 },
                new User { Id = 2, Email = "adminuser@gmail.com", Password = encryptPass.GetSHA256("admin"), Role_Id = 1 },
                new User { Id = 3, Email = "regularuser@gmail.com", Password = encryptPass.GetSHA256("regular"), Role_Id = 2 }
            );

            // Seed ProductCategory
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { Id = 1, ProductName = "Product Category 1" },
                new ProductCategory { Id = 2, ProductName = "Product Category 2" }
            );

            // Seed Product
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Price = 400, UploadDate = DateTime.Now, IdProductCategory_Id = 1 },
                new Product { Id = 2, Price = 400, UploadDate = DateTime.Now, IdProductCategory_Id = 2 }
            );
        }
    }
}
