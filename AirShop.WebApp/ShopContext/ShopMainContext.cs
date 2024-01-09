using User = AirShop.ExternalServices.Entities.User;

namespace AirShop.WebApp.ShopContext
{
    public class ShopMainContext
    {
        public bool IsUserLoggedIn { get; private set; }
        public User LoggedInUser { get; private set; }

        public ShopMainContext()
        {
            IsUserLoggedIn = false;
            LoggedInUser = new User()
            {
                Username = "guest",
                Password = "guest"
            };
        }

        public ShopMainContext(User loggedInUser)
        {
            LoggedInUser = loggedInUser;
        }

        public void LogInUser(User user)
        {
            IsUserLoggedIn = true;
            LoggedInUser = user;
        }

        public void LogOutUser()
        {
            IsUserLoggedIn = false;
            LoggedInUser = new User() 
            {
                Username = "guest",
                Password = "guest"
            };
        }
    }
}
