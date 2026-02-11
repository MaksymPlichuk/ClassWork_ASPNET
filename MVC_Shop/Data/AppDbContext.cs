using Microsoft.EntityFrameworkCore;
using MVC_Shop.Models;

namespace MVC_Shop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CategoryModel>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            });

            builder.Entity<ProductModel>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(e => e.Image)
                .HasMaxLength(255);

                entity.Property(e => e.Color)
                .HasMaxLength(100);

                entity.Property(e => e.Description)
                .HasColumnType("ntext");
            });

            builder.Entity<ProductModel>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
