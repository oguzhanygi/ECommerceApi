using ECommerceApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Data;

public class ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
    : IdentityDbContext<Customer, IdentityRole<Guid>, Guid>(options)
{
    // Products and Categories
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    
    // Orders and Shopping
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    
    // Customer related
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Review> Reviews { get; set; }
    
    // Inventory and Discounts
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    public DbSet<Discount> Discounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call base to set up Identity tables
        base.OnModelCreating(modelBuilder);

        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(c => c.Name).IsUnique();
            entity.HasIndex(c => c.ParentCategoryId);
            entity.HasIndex(c => c.Slug).IsUnique().HasFilter("[Slug] IS NOT NULL");

            entity.HasQueryFilter(c => !c.IsDeleted);
            
            entity.Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Product configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(p => p.Name);
            entity.HasIndex(p => p.Brand);
            entity.HasIndex(p => p.CategoryId);
            entity.HasIndex(p => p.SKU).IsUnique().HasFilter("[SKU] IS NOT NULL");

            entity.HasQueryFilter(p => !p.IsDeleted);

            entity.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
                
            // Configure collections as JSON columns
            entity.Property(p => p.ImageUrls)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions()),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, new System.Text.Json.JsonSerializerOptions()) ?? new List<string>());
                
            entity.Property(p => p.Tags)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions()),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, new System.Text.Json.JsonSerializerOptions()) ?? new List<string>());
        });
        
        // Customer configuration
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasQueryFilter(c => !c.IsDeleted);
            
            entity.HasMany(c => c.Addresses)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasMany(c => c.Reviews)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasMany(c => c.Wishlist)
                .WithMany()
                .UsingEntity(
                    "CustomerWishlist",
                    l => l.HasOne(typeof(Product)).WithMany().HasForeignKey("ProductId"),
                    r => r.HasOne(typeof(Customer)).WithMany().HasForeignKey("CustomerId"));
        });
        
        // Address configuration
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasOne(a => a.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CustomerId);
                
            entity.HasQueryFilter(a => !a.IsDeleted);
        });
        
        // Order configuration
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(o => o.OrderNumber).IsUnique();
            entity.HasIndex(o => o.CustomerId);
            entity.HasIndex(o => o.OrderDate);
            
            entity.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);
                
            entity.HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .IsRequired(false);
                
            entity.HasOne(o => o.BillingAddress)
                .WithMany()
                .HasForeignKey(o => o.BillingAddressId)
                .IsRequired(false);
                
            entity.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);
                
            entity.HasQueryFilter(o => !o.IsDeleted);
        });
        
        // OrderItem configuration
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
                
            entity.HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);
        });
        
        // Review configuration
        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);
                
            entity.HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId);
                
            entity.HasIndex(r => new { r.ProductId, r.CustomerId }).IsUnique();
            entity.HasQueryFilter(r => !r.IsDeleted);
        });
        
        // Cart configuration
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .IsRequired(false);
                
            entity.HasMany(c => c.Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);
                
            entity.HasIndex(c => c.SessionId);
            entity.HasIndex(c => c.CustomerId);
        });
        
        // CartItem configuration
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId);
                
            entity.HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId);
        });
        
        // Inventory configuration
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId);
                
            entity.HasIndex(i => i.ProductId).IsUnique();
            entity.HasIndex(i => i.SKU).IsUnique().HasFilter("[SKU] IS NOT NULL");
        });
        
        // InventoryTransaction configuration
        modelBuilder.Entity<InventoryTransaction>(entity =>
        {
            entity.HasOne(it => it.Inventory)
                .WithMany(i => i.Transactions)
                .HasForeignKey(it => it.InventoryId);
                
            entity.HasOne(it => it.Order)
                .WithMany()
                .HasForeignKey(it => it.OrderId)
                .IsRequired(false);
        });
        
        // Discount configuration
        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasIndex(d => d.Code).IsUnique();
            entity.HasQueryFilter(d => !d.IsDeleted);
            
            // Configure collections as JSON columns
            entity.Property(d => d.ApplicableProductIds)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions()),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<Guid>>(v, new System.Text.Json.JsonSerializerOptions()));
                    
            entity.Property(d => d.ApplicableCategoryIds)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions()),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<Guid>>(v, new System.Text.Json.JsonSerializerOptions()));
        });
    }
}