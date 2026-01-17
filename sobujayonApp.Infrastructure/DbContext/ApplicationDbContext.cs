using sobujayonApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace sobujayonApp.Infrastructure.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<DeliveryArea> DeliveryAreas { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<NavItem> NavItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Password).HasColumnName("password");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.HasKey(e => e.Id);
            entity.HasOne(p => p.Category)           // Product has one Category
                  .WithMany(c => c.Products)         // Category has many Products
                  .HasForeignKey(p => p.CategoryId)  // Use "CategoryId", NOT "CategoryId1"
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.Slug).IsUnique();
            entity.Property(e => e.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Slug).IsUnique();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            // Relationship: One Category -> Many Products
            entity.HasMany(c => c.Products)
                  .WithOne(p => p.Category)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
            // Restrict prevents deleting a category if it still has products
        });

        modelBuilder.Entity<Review>(entity => {
            entity.HasOne(r => r.Product).WithMany().HasForeignKey(r => r.ProductId);
            entity.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
        });

        modelBuilder.Entity<WishlistItem>(entity => {
            entity.HasOne(w => w.Product).WithMany().HasForeignKey(w => w.ProductId);
            entity.HasOne(w => w.User).WithMany().HasForeignKey(w => w.UserId);
        });

        modelBuilder.Entity<Cart>(entity => {
            entity.HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId);
        });

        modelBuilder.Entity<CartItem>(entity => {
            entity.HasOne(ci => ci.Cart).WithMany(c => c.Items).HasForeignKey(ci => ci.CartId);
            entity.HasOne(ci => ci.Product).WithMany().HasForeignKey(ci => ci.ProductId);
        });

        modelBuilder.Entity<Order>(entity => {
            entity.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId);
            entity.Property(o => o.Total).HasPrecision(18, 2);
        });

        modelBuilder.Entity<OrderItem>(entity => {
            entity.HasOne(oi => oi.Order).WithMany(o => o.Items).HasForeignKey(oi => oi.OrderId);
            entity.HasOne(oi => oi.Product).WithMany().HasForeignKey(oi => oi.ProductId);
            entity.Property(oi => oi.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Blog>(entity => {
             entity.HasIndex(b => b.Slug).IsUnique();
        });
        
        modelBuilder.Entity<NavItem>(entity => {
             entity.HasIndex(n => n.Slug).IsUnique();
        });
    }
}