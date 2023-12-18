using User = AirShop.ExternalServices.Entities.User;
using AirShop.WebApiPostgre.Data.Models;

namespace AirShop.WebApp.ShopContext
{
    public class ShopMainContext
    {
        public bool IsUserLoggedIn { get; private set; }
        public User LoggedInUser { get; private set; }

        public void LogInUser(User user)
        {
            IsUserLoggedIn = true;
            LoggedInUser = user;
        }

        public void LogOutUser()
        {
            IsUserLoggedIn = false;
            LoggedInUser = null;
        }
    }
}
