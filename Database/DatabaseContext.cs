using Data;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public DbSet<Proof> Proofs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./tasks.sqlite");
        }
    }
}
