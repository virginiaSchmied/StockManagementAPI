using Microsoft.EntityFrameworkCore;
using StockManagementAPI.Models.Product;
using StockManagementAPI.Models.User;
using System.Data;

namespace StockManagementAPI.DataAccess
{
    public class stockManagementDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public stockManagementDbContext(DbContextOptions<stockManagementDbContext> options, IConfiguration configuration) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductsCategories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Especificar al modelo que las tablas van en singular 
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategories");
            modelBuilder.Entity<Product>().ToTable("Products");

            // Seed Role
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, Name = "Admin", Description = "Rol de usuario con permisos especiales" },
                new UserRole { Id = 2, Name = "Regular", Description = "Rol de usuario sin permisos" }
            );

            // Seed User
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "schmiedvirginia@gmail.com", Password = encryptPass.GetSHA256("Virginia2024"), Role_Id = 1 },
                new User { Id = 2, Email = "usuarioadminisitrador@gmail.com", Password = encryptPass.GetSHA256("admin"), Role_Id = 1 },
                new User { Id = 3, Email = "usuarioregular@gmail.com", Password = encryptPass.GetSHA256("regular"), Role_Id = 2 }
            );

            // Seed ProductCategory
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { Id = 1, ProductName = "ProductCategory1" },
                new ProductCategory { Id = 2, ProductName = "ProductCategory2" }
            );

            // Seed Product
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Price = 400, UploadDate = DateTime.Now, IdProductCategory_Id = 1 },
                new Product { Id = 2, Price = 200, UploadDate = DateTime.Now, IdProductCategory_Id = 2 }
            );
        }
    }
}
