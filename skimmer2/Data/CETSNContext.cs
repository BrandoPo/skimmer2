using Microsoft.EntityFrameworkCore;
using skimmer2.Models;

namespace skimmer2.Data
{
    public class CETSNContext : DbContext
    {
        public CETSNContext(DbContextOptions<CETSNContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<account> Accounts { get; set; } // Add this line

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users"); // Ensure the table name matches
                entity.HasKey(e => e.Id); // Define the primary key
                entity.Property(e => e.Id).HasColumnName("Id"); // Map to the correct column name
                entity.Property(e => e.Username).HasColumnName("Username");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.Password).HasColumnName("Password");
            });

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