using AirShop.ExternalServices;
using AirShop.WebApiPostgre.Data.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.ExternalServices
{
    public class AirRestClient
    {
        private readonly IAirRestClientConfig restConfig;

        public AirRestClient(IAirRestClientConfig restConfig)
        {
            this.restConfig = restConfig;
        }

        public IEnumerable<Product> GetDevices()
        {
            var client = new RestClient(restConfig.BaseUrl);

            var request = new RestRequest("http://localhost:5000/api/Products", Method.Get);

            var result = client.Execute(request);

            if (!result.IsSuccessful)
                throw new RestClientException($"RestClient error({result.StatusCode}): {result.Content}", result.StatusCode, result.ErrorMessage);

            return JsonConvert.DeserializeObject<List<Product>>(result.Content) ?? new List<Product>();
        }
    }
}
