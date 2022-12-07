using Microsoft.EntityFrameworkCore;

namespace QueueAPI.Database
{
    public class QueueContext : DbContext
    {
        public QueueContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Entities.Entry> Entries { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Entry>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Entities.Entry>()
                .Property(e => e.WasConsumed)
                .HasDefaultValue(false);
        }
    }
}
