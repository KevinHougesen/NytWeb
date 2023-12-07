using NytWeb.Models;

namespace NytWeb.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserAsync(string User);

        Task<List<UserModel>> GetUsersAsync();

        Task<List<string>> GetUserFollowersAsync(string userId);

        Task<List<string>> GetUserFollowingAsync(string userId);

        Task<List<UserModel>> FollowUserAsync(string userId, string toFollow);

    }
}