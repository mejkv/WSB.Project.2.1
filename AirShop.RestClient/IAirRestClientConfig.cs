using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.ExternalServices
{
    public interface IAirRestClientConfig
    {
        public string BaseUrl { get; }
    }
}
