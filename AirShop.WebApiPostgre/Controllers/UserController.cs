using AirShop.WebApiPostgre.ApiServices;
using AirShop.WebApiPostgre.Data.Models.Requests;
using AirShop.WebApiPostgre.Data.ShopDbContext;
using Microsoft.AspNetCore.Mvc;

namespace AirShop.WebApiPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ShopDbContext _context;

        public UserController(IUserService userService, ShopDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginModel)
        {
            var user = await _userService.AuthenticateAsync(loginModel.Username, loginModel.Password);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel registerModel)
        {
            var user = await _userService.RegisterAsync(registerModel);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }
    }
}
