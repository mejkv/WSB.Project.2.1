using AirShop.WebApp.ShopContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AirShop.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ShopMainContext _shopMainContext;

        public IndexModel(ILogger<IndexModel> logger, ShopMainContext shopMainContext)
        {
            _logger = logger;
            _shopMainContext = shopMainContext;
        }

        public void OnGet()
        {
            ViewData["IsUserLoggedIn"] = _shopMainContext.IsUserLoggedIn;
            _logger.LogInformation("Is user logged? Result: "+_shopMainContext.IsUserLoggedIn.ToString());

            ViewData["LoggedInUserName"] = _shopMainContext.LoggedInUser?.Username;
        }
    }
}