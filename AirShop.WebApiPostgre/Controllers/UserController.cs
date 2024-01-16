using AirShop.DataAccess.Data.Models;
using AirShop.DataAccess.Data.Models.Requests;
using AirShop.WebApiPostgre.ApiServices;
using AirShop.WebApiPostgre.Data.ApiExceptions;
using Microsoft.AspNetCore.Mvc;

namespace AirShop.WebApiPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginModel)
        {
            User user;
            try
            {
                user = await _userService.AuthenticateAsync(loginModel.Username, loginModel.Password);
            }
            catch (UserNotExistException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel registerModel)
        {
            var user = await _userService.RegisterAsync(registerModel);

            return Ok(user);
        }
    }
}
