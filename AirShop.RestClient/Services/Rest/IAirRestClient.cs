using AirShop.DataAccess.Data.Models;
using User = AirShop.ExternalServices.Entities.User;

namespace AirShop.ExternalServices.Services.Rest
{
    public interface IAirRestClient
    {
        IEnumerable<Product> GetDevices();
        User GetUser(string login, string password);
    }
}
