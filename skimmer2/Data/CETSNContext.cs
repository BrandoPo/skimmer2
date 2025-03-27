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
    }
}