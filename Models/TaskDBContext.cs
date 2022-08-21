using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Models
{
    public class EFTaskDBContext : DbContext
    {
        public EFTaskDBContext(DbContextOptions<EFTaskDBContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
    }
}
