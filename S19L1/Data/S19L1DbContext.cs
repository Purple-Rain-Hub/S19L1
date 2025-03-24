using Microsoft.EntityFrameworkCore;
using S19L1.Models;

namespace S19L1.Data
{
    public class S19L1DbContext : DbContext
    {
        public S19L1DbContext(DbContextOptions<S19L1DbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
