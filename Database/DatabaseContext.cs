using Data;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    /// <summary>
    /// Database context represents the database.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        public DbSet<Task> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the proofs.
        /// </summary>
        /// <value>The proofs.</value>
        public DbSet<Proof> Proofs { get; set; }

        /// <summary>
        /// Is called when the database context is configured. Sets the Database path.
        /// </summary>
        /// <param name="optionsBuilder">Options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=/Users/simonangerbauer/Documents/tasks.sqlite");
        }
    }
}
