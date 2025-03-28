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
        }

     
    }
}