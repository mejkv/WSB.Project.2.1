using AirShop.DataAccess.Data.Models;
using AirShop.ExternalServices.Entities;
using AirShop.ExternalServices.Services.Rest.RequestBody;
using AirShop.ExternalServices.Services.Rest.RestExceptions;
using Newtonsoft.Json;
using RestSharp;
using User = AirShop.ExternalServices.Entities.User;

namespace AirShop.ExternalServices.Services.Rest
{
    public class AirRestClient : IAirRestClient
    {
        private readonly IAirRestClientConfig restConfig;

        public AirRestClient(IAirRestClientConfig restConfig)
        {
            this.restConfig = restConfig ?? throw new ArgumentNullException(nameof(restConfig));
        }

        public IEnumerable<Product> GetDevices()
        {
            var client = new RestClient(restConfig.BaseUrl);

            var request = new RestRequest("http://localhost:5000/api/Products", Method.Get);

            var result = client.Execute<IEnumerable<Product>>(request);

            if (!result.IsSuccessful)
                throw new RestClientException($"RestClient error({result.StatusCode}): {result.Content}", result.StatusCode, GetNotNullErrorMessage(result.ErrorMessage));

            return JsonConvert.DeserializeObject<List<Product>>(GetNotNullContent(result.Content)) ?? Enumerable.Empty<Product>();
        }

        public User GetUser(string login, string password)
        {
            var client = new RestClient(restConfig.BaseUrl);

            var request = new RestRequest("http://localhost:5000/api/User/login", Method.Post);

            var loginRequest = new LoginRequest
            {
                Username = login,
                Password = password
            };

            var jsonBody = JsonConvert.SerializeObject(loginRequest);
            request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

            var result = client.Execute<User>(request);

            if (!result.IsSuccessful)
                throw new RestClientException($"RestClient error({result.StatusCode}): {result.Content}", result.StatusCode, GetNotNullErrorMessage(result.ErrorMessage));
            
            return JsonConvert.DeserializeObject<User>(GetNotNullContent(result.Content)) ?? throw new JsonSerializationException("Deserialization of User failed.");
        }

        private string GetNotNullContent(string? content)
        {
            return string.IsNullOrWhiteSpace(content) ? string.Empty : content;
        }

        private string GetNotNullErrorMessage(string? message)
        {
            return string.IsNullOrWhiteSpace(message) ? string.Empty : message;
        }
    }
}
