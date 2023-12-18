using AirShop.ExternalServices.Entities;
using AirShop.ExternalServices.Services.Rest;
using AirShop.WebApiPostgre.Data.Models;
using AirShop.WebApp.ShopContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using User = AirShop.ExternalServices.Entities.User;

namespace AirShop.WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ShopMainContext _shopMainContext;

        public LoginModel(ShopMainContext shopMainContext)
        {
            _shopMainContext = shopMainContext;
        }

        [BindProperty]
        public LoginInputModel LoginInput { get; set; }

        public class LoginInputModel
        {
            [Required(ErrorMessage = "Please enter your username.")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Please enter your password.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public IActionResult OnPost()
        {
            if (IsUserExist())
            {
                var loggedInUser = new User { Username = LoginInput.Username }; 
                _shopMainContext.LogInUser(loggedInUser);

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }

        private bool IsUserExist()
        {
            var client = new AirRestClient(new AirRestClientConfig());
            var validUser = client.GetUser(LoginInput.Username, LoginInput.Password).FirstOrDefault();

            return !(validUser is null);
        }
    }
}