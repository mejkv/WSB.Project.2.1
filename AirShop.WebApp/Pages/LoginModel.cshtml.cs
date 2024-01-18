using AirShop.ExternalServices.Entities;
using AirShop.ExternalServices.Services.Rest;
using AirShop.ExternalServices.Services.Rest.RestExceptions;
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
        private readonly IAirRestClient _client;

        public LoginModel(ShopMainContext shopMainContext, IAirRestClient client)
        {
            _shopMainContext = shopMainContext;
            _client = client;
            
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
            try
            {
                var validUser = _client.GetUser(LoginInput.Username, LoginInput.Password);

                return !(validUser is null);
            }
            catch (RestClientException exception)
            {
                //Doda? mo?e jakie? logowanie czy co? podobnego
                return false;
            }
        }
    }

    public class LoginInputModel
    {
        [Required(ErrorMessage = "Please enter your username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}