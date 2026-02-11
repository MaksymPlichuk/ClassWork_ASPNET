using Microsoft.EntityFrameworkCore;
using MVC_Shop.Models;

namespace MVC_Shop.Data.Helpers
{
    public static class DbInitializer
    {
        public static void SeedProducts (this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel { Id = 1, Name = "Zowie S2-DW 4K Wireless Mouse", Description = "SYMMETRICAL GAMING WIRELESS MOUSE FOR ESPORTS", Price = 500, Amount = 50, Color = "Black", CategoryId = 1 }
            );
        }
        public static void SeedCategories(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<CategoryModel>().HasData(
                    new CategoryModel { Id = 1, Name = "Electronics" },
                    new CategoryModel { Id = 2, Name = "Books" },
                    new CategoryModel { Id = 3, Name = "Clothing" }
                );
        }
    }
}
