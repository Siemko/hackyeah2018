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
        public DbSet<SectionIssue> SectionIssue { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SectionIssue>().HasKey(t => new { t.IssueId, t.SectionId });
            modelBuilder.Entity<SectionIssue>().HasOne(bs => bs.Section).WithMany(b => b.SectionIssues).HasForeignKey(b => b.SectionId);
            modelBuilder.Entity<SectionIssue>().HasOne(bs => bs.Issue).WithMany(b => b.SectionIssues).HasForeignKey(b => b.IssueId);


            base.OnModelCreating(modelBuilder);
        }
        public void Migrate()
        {
            Database.Migrate();
        }
    }
}
