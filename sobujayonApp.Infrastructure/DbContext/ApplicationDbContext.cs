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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users", "dbo");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Email).HasColumnName("Email");
            entity.Property(e => e.Phone).HasColumnName("Phone");
            entity.Property(e => e.Name).HasColumnName("Name");
            entity.Property(e => e.Address).HasColumnName("Address");
            entity.Property(e => e.Dob).HasColumnName("Dob");
            entity.Property(e => e.Gender).HasColumnName("Gender");
            entity.Property(e => e.Password).HasColumnName("Password");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
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
            entity.ToTable("Categories");
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
    }
}