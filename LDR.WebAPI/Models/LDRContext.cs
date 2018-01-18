using Microsoft.EntityFrameworkCore;

namespace LDR.WebAPI.Models
{
    public class LDRContext : DbContext
    {
        public LDRContext(DbContextOptions<LDRContext> options)
            : base(options)
        {
        }

        public DbSet<Point> Points { get; set; }
    }
}
