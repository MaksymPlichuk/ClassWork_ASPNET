using Microsoft.EntityFrameworkCore;
using MVC_Shop.Models;
using System.Text.Json;

namespace MVC_Shop.Data.Initializer
{
    public static class Seeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.Migrate();

            if (!context.Categories.Any() || context.Categories == null)
            {
                string root = Directory.GetCurrentDirectory();
                string path = Path.Combine(root, "wwwroot", "seed_data", "components.json");
                string json = File.ReadAllText(path);
                List<CategoryModel>? categories = JsonSerializer.Deserialize<List<CategoryModel>>(json);

                if (categories == null)
                {
                    return;
                }
                context.Categories.AddRange(categories);
                context.SaveChanges();

            }
        }
    }
}
