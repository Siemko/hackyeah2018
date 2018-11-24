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

        public void Migrate()
        {
            Database.Migrate();
        }
    }
}
