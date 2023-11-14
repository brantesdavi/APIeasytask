using Microsoft.EntityFrameworkCore;

namespace APIeasytask.Models
{
    public class APIDbContext: DbContext
    {
        public APIDbContext(DbContextOptions option): base(option) { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set;}
        public DbSet<Priority> Priorities { get; set; }
    }
}
