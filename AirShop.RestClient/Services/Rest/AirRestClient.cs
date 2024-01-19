using AirShop.DataAccess.Data.Models;
using AirShop.ExternalServices.Entities;
using AirShop.ExternalServices.Services.Rest.RequestBody;
using AirShop.ExternalServices.Services.Rest.RestExceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        public RestResponse PostDocument(Entities.Document invoice)
        {
            var client = new RestClient(restConfig.BaseUrl);

            var request = new RestRequest("http://localhost:5000/api/Document", Method.Post);

            var serializer = new JsonSerializerOptions 
            { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 256,
            };
            var body = ChangeDocumentEntityAsString(invoice);
            //var jsonBody = System.Text.Json.JsonSerializer.Serialize(doc, serializer);

            //request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
            request.AddJsonBody(body);

            var result = client.Execute<DataAccess.Data.Models.Document>(request);

            if (!result.IsSuccessful)
                throw new RestClientException($"RestClient error({result.StatusCode}): {result.Content}", result.StatusCode, GetNotNullErrorMessage(result.ErrorMessage));
            
            return result;
        }

        //TODO: Do przeniesienia
        private string ChangeDocumentEntityAsString(Entities.Document document)
        {
            var newDocument = new
            {
                userId = document.User.Id,
                issueDate = DateTime.UtcNow,
                contractor = new
                {
                    name = document.Customer.Name,
                    firstName = document.Customer.FirstName,
                    lastName = document.Customer.LastName,
                    street = document.Customer.Street,
                    city = document.Customer.City,
                    houseNumber = document.Customer.HouseNumber,
                    apartmentNumber = document.Customer.ApartmentNumber,
                    postalCode = document.Customer.PostalCode,
                    nip = document.Customer.Nip,
                    userId = 0,
                    user = new
                    {
                        id = document.User.Id,
                        username = document.User.Username,
                        email = document.User.Email,
                        password = document.User.Password
                    }
                },
                documentPositions = document.DocumentPositions.Select(dp => new
                {
                    documentPositionId = 0,
                    documentId = 0,
                    productId = dp.ProductId,
                    quantity = dp.Quantity,
                    unitPrice = dp.UnitPrice,
                    product = new
                    {
                        productId = dp.Product.ProductId,
                        name = dp.Product.Name,
                        value = dp.Product.Value,
                        vat = dp.Product.Vat,
                        price = dp.Product.Price,
                        discount = dp.Product.Discount,
                        code = new
                        {
                            id = dp.Product.Code.Id,
                            productId = dp.ProductId,
                            ean = dp.Product.Code.Ean
                        },
                        productType = dp.Product.ProductType,
                        image = dp.Product.Image
                    }
                }).ToList()
            };

            return System.Text.Json.JsonSerializer.Serialize(newDocument, new JsonSerializerOptions
            {
                WriteIndented = false,
            });
        }
        private string ChangeReceiptEntityAsString(DataAccess.Data.Models.Receipt receipt)
        {
            var newReceipt = new
            {
                name = string.IsNullOrWhiteSpace(receipt.Name) ? "DefaultReceiptName" : receipt.Name,
                value = receipt.Value,
                vat = receipt.Vat,
                price = receipt.Price,
                discount = receipt.Discount,
                isSimplifiedInvoice = receipt.IsSimplifiedInvoice,
                receiptPositions = receipt.ReceiptPositions.Select(rp => new
                {
                    productId = rp.ProductId,
                    product = new
                    {
                        productId = rp.Product.ProductId,
                        name = rp.Product.Name,
                        value = rp.Product.Value,
                        vat = rp.Product.Vat,
                        price = rp.Product.Price,
                        discount = rp.Product.Discount,
                        code = new
                        {
                            id = rp.Product.Code.Id,
                            productId = rp.ProductId,
                            ean = rp.Product.Code.Ean
                        },
                        productType = rp.Product.ProductType,
                        image = rp.Product.Image
                    },
                    quantity = rp.Quantity,
                    totalPrice = rp.TotalPrice
                }).ToList()
            };

            // Tworzymy string JSON z obiektu newReceipt
            return System.Text.Json.JsonSerializer.Serialize(newReceipt, new JsonSerializerOptions
            {
                WriteIndented = false,
            });
        }
        public RestResponse PostReceipt(DataAccess.Data.Models.Receipt receipt)
        {
            var client = new RestClient(restConfig.BaseUrl);

            var request = new RestRequest("http://localhost:5000/api/Receipt", Method.Post);

            var body = ChangeReceiptEntityAsString(receipt);

            request.AddJsonBody(body);

            var result = client.Execute<DataAccess.Data.Models.Receipt>(request);

            if (!result.IsSuccessful)
                throw new RestClientException($"RestClient error({result.StatusCode}): {result.Content}", result.StatusCode, GetNotNullErrorMessage(result.ErrorMessage));

            return result;
        }

        public User RegisterUser(string login, string password, string email)
        {
            var client = new RestClient(restConfig.BaseUrl);

            var request = new RestRequest("http://localhost:5000/api/User/login", Method.Post);

            var loginRequest = new RegisterRequest
            {
                Username = login,
                Password = password,
                Email = email
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
