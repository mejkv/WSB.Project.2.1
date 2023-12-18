using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirShop.ExternalServices.Entities
{
    public class Device
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }

        [JsonProperty("vat")]
        public decimal Vat { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }
    }
}
