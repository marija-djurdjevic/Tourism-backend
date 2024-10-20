using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<TourEquipment> TourEquipment{ get; set; }
    public DbSet<TourProblem> TourProblems { get; set; }
    public DbSet<TourPreferences> TourPreferences { get; set; }
    public DbSet<Tour> Tour { get; set; }
    public DbSet<KeyPoint> KeyPoints { get; set; }
    public DbSet<Explorer.Tours.Core.Domain.Object> Object { get; set; }

    public DbSet<TourReview> TourReview { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
    }
}