using AirShop.WebApiPostgre.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.ExternalServices
{
    public interface IAirRestClient
    {
        IEnumerable<Product> GetDevices();
    }
}
