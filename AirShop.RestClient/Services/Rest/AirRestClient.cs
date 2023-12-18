using AirShop.ExternalServices.Entities;
using AirShop.ExternalServices.Services.Rest.RequestBody;
using AirShop.ExternalServices.Services.Rest.RestExceptions;
using Newtonsoft.Json;
using RestSharp;
using Product = AirShop.WebApiPostgre.Data.Models.Product;

namespace AirShop.ExternalServices.Services.Rest
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

        public IEnumerable<User> GetUser(string login, string password)
        {
            var client = new RestClient(restConfig.BaseUrl);

            var request = new RestRequest("http://localhost:5000/api/User/login", Method.Post);

            var loginRequest = new LoginRequest() 
            {
                Username = login,
                Password = password
            };

            var jsonBody = JsonConvert.SerializeObject(loginRequest);
            request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

            var result = client.Execute(request);

            if (!result.IsSuccessful)
                throw new RestClientException($"RestClient error({result.StatusCode}): {result.Content}", result.StatusCode, result.ErrorMessage);
            
            yield return JsonConvert.DeserializeObject<User>(result.Content);
        }
    }
}