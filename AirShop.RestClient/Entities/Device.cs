using Newtonsoft.Json;

namespace AirShop.ExternalServices.Entities
{
    public class Device
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }

        [JsonProperty("vat")]
        public decimal Vat { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }

        [JsonProperty("code")]
        public EanCode Code { get; set; }
    }

    public class EanCode
    {
        [JsonProperty("ean")]
        public string Ean { get; set; }
    }
}
