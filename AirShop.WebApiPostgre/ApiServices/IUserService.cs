using AirShop.WebApiPostgre.Data.Models;
using AirShop.WebApiPostgre.Data.Models.Requests;

namespace AirShop.WebApiPostgre.ApiServices
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(RegisterRequestModel model);
    }
}
