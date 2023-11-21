using NytWeb.Models;
using NytWeb.Data;
using NytWeb.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using System.Text;

namespace NytWeb.Services
{
    public class UserService : IUserService
    {
        private readonly ApiContext _context;
        private IConfiguration _config;

        // Database Constructor
        public UserService(ApiContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        // Get All Users
        public async Task<List<UserModel>> GetUsersAsync()
        {
            var users = await _context.GetUsersAsync();
            return users;
        }

        public async Task<List<PostModel>> GetUsersFeedAsync()
        {
            var posts = await _context.GetPostsAsync();
            return posts;
        }

        // Get Singular User
        public async Task<UserModel> GetUserAsync(string User)
        {
            var user = await _context.GetUserAsync(User);
            return user;
        }

        public async Task<UserModel> LoginAsync(string Username, string Password)
        {
            // Get User from Database
            var user = await _context.GetUserAsync(Username);

            if (user == null)
                return null;

            var isMatch = BCrypt.Net.BCrypt.EnhancedVerify(Password, user.Password);
            if (isMatch == false)
                return null;

            var tokenString = GenerateToken(user);
            //var response = new JsonResult(Ok(new { token = tokenString }));
            return user;
        }

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),

            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<UserModel> AddUser(UserModel user)
        {

            // Check if User already exists in Database
            var usersInDb = await _context.GetUsersAsync();
            var findUser = usersInDb.Where(l => l.Username == user.Username).FirstOrDefault();



            if (findUser == null)
            {
                // Validate the user object.
                if (string.IsNullOrEmpty(user.Username))
                {
                    return null;
                }
                if (string.IsNullOrEmpty(user.Email))
                {
                    return null;
                }
                if (string.IsNullOrEmpty(user.Password))
                {
                    return null;
                }

                // Hash the user's password before saving it to the database.
                var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password);
                user.Password = passwordHash;

                // Assign User Id
                user.Id = user.Username;

                // Assign User Authorization
                user.Role = "User";

                await _context.CreateUserAsync(user);

            }
            else
            {
                // Update the user in the database.
                if (findUser == null)
                    return null;

                findUser = user;
            }
            return user;
        }



    }
}