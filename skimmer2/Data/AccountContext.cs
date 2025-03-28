using Microsoft.EntityFrameworkCore;
using skimmer2.Models;

namespace skimmer2.Data
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }

        public DbSet<account> Accounts { get; set; } // Changed from Users to Accounts

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account>(entity =>
            {
                entity.ToTable("account"); // Matches your table name
                entity.HasKey(e => e.id); // Define the primary key
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.username).HasColumnName("username");
                entity.Property(e => e.email).HasColumnName("email");
                entity.Property(e => e.password).HasColumnName("password");
                entity.Property(e => e.first_name).HasColumnName("first_name");
                entity.Property(e => e.last_name).HasColumnName("last_name");
                entity.Property(e => e.address).HasColumnName("address");
                entity.Property(e => e.Role).HasColumnName("Role")
                    .HasConversion<string>(); // Convert enum to string for database storage
            });
        }
    }
}