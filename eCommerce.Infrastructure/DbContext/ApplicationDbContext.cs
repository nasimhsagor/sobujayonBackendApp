using eCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users", "dbo");
            entity.HasKey(e => e.UserID);
            entity.Property(e => e.UserID).HasColumnName("UserID");
            entity.Property(e => e.Email).HasColumnName("Email");
            entity.Property(e => e.PersonName).HasColumnName("PersonName");
            entity.Property(e => e.Gender).HasColumnName("Gender");
            entity.Property(e => e.Password).HasColumnName("Password");
        });
    }
}