using MagazynEdu.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynEdu.DataAccess.CQRS.Queries
{
    public class GetDevicesQuery : QueryBase<List<Device>>
    {

        public string Title { get; set; }

        public override Task<List<Device>> Execute(WarehouseStorageContext context)
        {
            //context.Devices.ToListAsync();
            return context.Devices.Where(x => x.Title == this.Title).ToListAsync();
        }
    }
}
