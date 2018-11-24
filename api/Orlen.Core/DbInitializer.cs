using Orlen.Core.Entities;
using System.Linq;

namespace Orlen.Core
{
    public static class DbInitializer
    {
        public static void Migrate(DataContext db)
        {
            db.Migrate();

            if (!db.IssueTypes.Any())
            {
                db.IssueTypes.Add(new IssueType() { Name = "MaxHeight" });
                db.IssueTypes.Add(new IssueType() { Name = "MaxWidth" });
                db.IssueTypes.Add(new IssueType() { Name = "MaxLength", });
                db.IssueTypes.Add(new IssueType() { Name = "MaxWeight" });
                db.SaveChanges();
            }

        }
    }
}
