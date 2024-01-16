using AirShop.DataAccess.Data.Models;
using AirShop.DataAccess.Data.Models.Requests;
using AirShop.DataAccess.Data.ShopDbContext;
using AirShop.WebApiPostgre.Data.ApiExceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AirShop.WebApiPostgre.ApiServices
{
    public class UserService : IUserService
    {
        private readonly ShopDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(ShopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            return user is null ? GetUserNotExistMessage(user.Username) : user;
        }

        private User GetUserNotExistMessage(string username)
        {
            throw new UserNotExistException($"User {username}: Wrong username or password", innerException: new Exception());
        }

        public async Task<User> RegisterAsync(RegisterRequestModel model)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (existingUser != null)
            {
                throw new UserNotExistException();
            }

            var newUser = new User() 
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
            };


            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<User>(newUser);
        }
    }
}
