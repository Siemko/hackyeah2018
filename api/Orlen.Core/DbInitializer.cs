namespace Orlen.Core
{
    public static class DbInitializer
    {
        public static void Migrate(DataContext db)
        {
            db.Migrate();
        }
    }
}
