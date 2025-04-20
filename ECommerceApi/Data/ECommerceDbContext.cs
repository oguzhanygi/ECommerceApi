using ECommerceApi.Models.Categories;
using ECommerceApi.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Data;

public class ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }

    // public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    // public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(c => c.Name).IsUnique();
            entity.HasIndex(c => c.ParentCategoryId);

            entity.HasQueryFilter(c => !c.IsDeleted);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(p => p.Name);
            entity.HasIndex(p => p.Brand);
            entity.HasIndex(p => p.CategoryId);

            entity.HasQueryFilter(p => !p.IsDeleted);

            entity.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}