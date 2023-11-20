using NytWeb.Models;
using NytWeb.Services;

namespace NytWeb.Services
{
    public interface IUserService
    {
        Task<UserModel> AddUser(UserModel user);
        Task<List<UserModel>> GetUsersAsync();

        Task<UserModel> GetUserAsync(string User);

        Task<UserModel> LoginAsync(string Email, string Password);
    }
}