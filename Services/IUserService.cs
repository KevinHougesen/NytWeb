using NytWeb.Models;

namespace NytWeb.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserAsync(string Username);

        Task<List<UserModel>> GetUsersAsync();

        Task<List<string>> GetUserFollowersAsync(string Username);

        Task<List<string>> GetUserFollowingAsync(string Username);

        Task<string> FollowUserAsync(string Username, string ToFollow);

    }
}