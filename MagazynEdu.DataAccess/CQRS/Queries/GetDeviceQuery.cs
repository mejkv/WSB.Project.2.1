using MagazynEdu.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynEdu.DataAccess.CQRS.Queries
{
    public class GetDeviceQuery : QueryBase<Device>
    {

        public int Id { get; set; }

        public override async Task<Device> Execute(WarehouseStorageContext context)
        {
            var device = await context.Devices.FirstOrDefaultAsync(x => x.Id == this.Id);
            return device;
        }
    }
}
