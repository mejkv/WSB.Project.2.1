using MagazynEdu.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynEdu.DataAccess.CQRS.Commands
{
    public class AddDeviceCaseCommand : CommandBase<DeviceCase, DeviceCase>
    {
        public override async Task<DeviceCase> Execute(WarehouseStorageContext context)
        {
            await context.DeviceCases.AddAsync(this.Parameter);
            await context.SaveChangesAsync();
            return this.Parameter;
        }
    }
}
