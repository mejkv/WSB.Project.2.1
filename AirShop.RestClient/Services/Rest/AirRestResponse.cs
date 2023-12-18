using AirShop.ExternalServices.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.ExternalServices.Services.Rest
{
    public class AirRestResponse : RestResponse
    {
        public IEnumerable<Device> Devices { get; set; }
    }
}
