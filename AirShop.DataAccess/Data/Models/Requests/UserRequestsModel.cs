namespace AirShop.DataAccess.Data.Models.Requests
{
    public class LoginRequestModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class RegisterRequestModel
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}