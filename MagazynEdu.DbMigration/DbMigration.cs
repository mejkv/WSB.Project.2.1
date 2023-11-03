using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MagazynEdu.DbMigration
{
    public class DbMigration
    {
        public void ExecuteUpgrade()
        {
            // Odnajdź wszystkie klasy UpgradeX
            var upgradeClasses = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.IsClass && type.Name.StartsWith("Upgrade"))
                .OrderBy(type => type.Name);

            // Wywołaj metodę Execute na każdej znalezionej klasie
            foreach (var upgradeClass in upgradeClasses)
            {
                var methodInfo = upgradeClass.GetMethod("Execute");
                if (methodInfo != null)
                {
                    // Jeśli metoda Execute istnieje, utwórz instancję klasy i wywołaj metodę
                    var instance = Activator.CreateInstance(upgradeClass);
                    methodInfo.Invoke(instance, null);
                }
            }
        }
    }
}
