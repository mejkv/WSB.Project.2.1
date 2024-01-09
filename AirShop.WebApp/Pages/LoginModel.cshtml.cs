using AirShop.ExternalServices.Entities;
using AirShop.ExternalServices.Services.Rest;
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
            LoginInput = new LoginInputModel() 
            {
                Password = string.Empty,
                Username = string.Empty
            };
        }

        [BindProperty]
        public LoginInputModel LoginInput { get; set; }

        public IActionResult OnPost()
        {
            if (IsUserExist())
            {
                var loggedInUser = new User { Username = LoginInput.Username, Password = LoginInput.Password }; 
                _shopMainContext.LogInUser(loggedInUser);

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }

        private bool IsUserExist()
        {
            var client = new AirRestClient(new AirRestClientConfig());
            var validUser = client.GetUser(LoginInput.Username, LoginInput.Password);

            return !(validUser is null);
        }
    }

    public class LoginInputModel
    {
        [Required(ErrorMessage = "Please enter your username.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}