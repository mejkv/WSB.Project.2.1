using System.Collections.Generic;

namespace MagazynEdu.DataAccess.Entities
{
    public  class DeviceCase : EntityBase
    {
        public int Number { get; set; }

        public List<Device> Devices { get; set; }
    }
}
