using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourSessions;
using Explorer.Tours.Core.Domain.TourProblems;
using Microsoft.EntityFrameworkCore;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Domain.PublishRequests;


namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<TourEquipment> TourEquipment{ get; set; }
    public DbSet<TourPreferences> TourPreferences { get; set; }
    public DbSet<Tour> Tour { get; set; }
    public DbSet<KeyPoint> KeyPoints { get; set; }
    public DbSet<Explorer.Tours.Core.Domain.Object> Object { get; set; }

    public DbSet<TourReview> TourReview { get; set; }

    public DbSet<TourSession> TourSessions { get; set; }
    public DbSet<Core.Domain.TourProblems.TourProblem> TourProblems { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public DbSet<PublishRequest> PublishRequests { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        //modelBuilder.Entity<ShoppingCart>().Property(item => item.Tokens).HasColumnType("jsonb");

        modelBuilder.Entity<TourSession>().Property(ts => ts.CurrentLocation) .HasColumnType("jsonb");
        modelBuilder.Entity<TourSession>().Property(ts => ts.CompletedKeyPoints).HasColumnType("jsonb");
        modelBuilder.Entity<KeyPoint>().Property(item => item.Coordinates).HasColumnType("jsonb");
        modelBuilder.Entity<Tour>().Property(item => item.TransportInfo).HasColumnType("jsonb");
        modelBuilder.Entity<Core.Domain.TourProblems.TourProblem>().Property(item => item.Details).HasColumnType("jsonb");
        modelBuilder.Entity<Core.Domain.TourProblems.TourProblem>().Property(item => item.Comments).HasColumnType("jsonb");
    }
}