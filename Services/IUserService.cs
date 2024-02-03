using NytWeb.Models;

namespace NytWeb.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserAsync(string Username);

        Task<List<UserModel>> GetUsersAsync();

        Task<List<string>> GetUserFollowersAsync(string Username);

        Task<List<string>> GetUserFollowingAsync(string Username);
        bool VerifyPass(string Password, string userAccountPassword);

        Task<bool> FollowUserAsync(string Username, string ToFollow);
        Task<List<string>> GetUserCommunitiesAsync(string Username);
        Task<InstagramMediaResponse> GetUserInstaMediaAlbumChildrenAsync(string id, string token);
        Task<InstagramAuthResponse> GetUserInstaTokenAsync(string code);
        Task<IgLongToken> GetUserInstaLongTokenAsync(string token);
        Task<InstagramMediaResponse> GetUserInstaMediaAsync(string token);
        Task<string> UpdateInstaTokenAsync(string Username, string Token);
        Task<InstagramMediaData> GetUserInstaMediaDataAsync(string id, string token);
        Task<List<string>> GetUserSeenPostAsync(string Username);

    }
}