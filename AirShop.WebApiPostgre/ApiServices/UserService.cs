using AirShop.WebApiPostgre.Data.Models;
using AirShop.WebApiPostgre.Data.Models.Requests;
using AirShop.WebApiPostgre.Data.ShopDbContext;
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
            return user;
        }

        public async Task<User> RegisterAsync(RegisterRequestModel model)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (existingUser != null)
            {
                return null;
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
