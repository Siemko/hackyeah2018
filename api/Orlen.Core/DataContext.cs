using Microsoft.EntityFrameworkCore;
using Orlen.Core.Entities;

namespace Orlen.Core
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueType> IssueTypes { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RoutePoint> RoutePoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoutePoint>().HasKey(t => new { t.RouteId, t.PointId });
            modelBuilder.Entity<RoutePoint>().HasOne(bs => bs.Point).WithMany(b => b.RoutePoints).HasForeignKey(b => b.PointId);
            modelBuilder.Entity<RoutePoint>().HasOne(bs => bs.Route).WithMany(b => b.RoutePoints).HasForeignKey(b => b.RouteId);

            base.OnModelCreating(modelBuilder);
        }
        public void Migrate()
        {
            Database.Migrate();
        }
    }
}
