using AirShop.DataAccess.Data.Models;

namespace AirShop.ExternalServices.Services.Rest
{
    public interface IAirRestClient
    {
        IEnumerable<Product> GetDevices();
    }
}
