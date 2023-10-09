using MagazynEdu.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagazynEdu.DataAccess
{
    public class WarehouseStorageContext : DbContext
    {
        public WarehouseStorageContext(DbContextOptions<WarehouseStorageContext> opt)
            :base(opt) 
        {
        }

        public DbSet<Device> Devices { get; set; }

        public DbSet<DeviceCase> DeviceCases { get; set; }

        public DbSet<Author> Authors { get; set; }
    }
}
