using AirShop.DataAccess.Data.Models;
using RestSharp;
using System.Reflection.Emit;
using User = AirShop.ExternalServices.Entities.User;

namespace AirShop.ExternalServices.Services.Rest
{
    public interface IAirRestClient
    {
        IEnumerable<Product> GetDevices();
        RestResponse PostDocument(Entities.Document invoice);
        RestResponse PostReceipt(Receipt receipt);
        User GetUser(string login, string password);
    }
}
