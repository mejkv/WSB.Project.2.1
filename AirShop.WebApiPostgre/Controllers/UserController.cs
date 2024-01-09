using AirShop.DataAccess.Data.Models.Requests;
using AirShop.WebApiPostgre.ApiServices;
using Microsoft.AspNetCore.Mvc;

namespace AirShop.WebApiPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
