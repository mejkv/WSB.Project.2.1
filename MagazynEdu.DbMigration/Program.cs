namespace MagazynEdu.DbMigration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "/u")
            {
                // Inicjalizacja i wykonanie migracji
                var migration = new DbMigration();
                migration.ExecuteUpgrade();
            }
            else
            {
                Console.WriteLine("Użycie: DbUpgrade.exe /u");
            }
        }
    }
}