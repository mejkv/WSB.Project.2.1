
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MagazynEdu.DataAccess
{
    public class WarehouseStorageContextFactory : IDesignTimeDbContextFactory<WarehouseStorageContext>
    {
        public WarehouseStorageContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WarehouseStorageContext>();
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS01;Initial Catalog=WarehouseStorage; Integrated Security=True; Trust Server Certificate=True;");
            return new WarehouseStorageContext(optionsBuilder.Options);
        }
    }
}
