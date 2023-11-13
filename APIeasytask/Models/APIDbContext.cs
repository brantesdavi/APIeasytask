using Microsoft.EntityFrameworkCore;

namespace APIeasytask.Models
{
    public class APIDbContext: DbContext
    {
        public APIDbContext(DbContextOptions option): base(option) { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Substask> Substasks { get; set;}
        public DbSet<Priority> Priorities { get; set; }
    }
}
