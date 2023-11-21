using NytWeb.Models;
using NytWeb.Services;

namespace NytWeb.Services
{
    public interface IUserService
    {
        Task<UserModel> AddUser(UserModel user);
        Task<List<UserModel>> GetUsersAsync();

        Task<List<PostModel>> GetUsersFeedAsync();

        Task<UserModel> GetUserAsync(string User);

        Task<UserModel> LoginAsync(string Email, string Password);

        Task<List<PostModel>> DisplayFeedAsync(string userId);
    }
}