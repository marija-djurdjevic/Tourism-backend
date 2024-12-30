using Explorer.Encounters.Core.Domain.EncounterExecutions;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.Secrets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database
{
    public class EncountersContext : DbContext
    {
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<EncounterExecution> EncountersExecutions { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<StoryUnlocked> StoriesUnlocked { get; set; }

        public EncountersContext(DbContextOptions<EncountersContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("encounters");
            modelBuilder.Entity<Encounter>().Property(item => item.Coordinates).HasColumnType("jsonb");
        }
    }
}
