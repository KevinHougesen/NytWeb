using NytWeb.Models;

namespace NytWeb.Services
{
    public interface IAuthService
    {
        Task<UserModel> RegisterAsync(UserModel User, string Picture);

        Task<string> DeleteUserByIdAsync(string Username);

        Task<string> DeleteUserAsync(string Username);

        Task<string> ConnectToHub(string Username, List<string> groupName);

        Task<string> AuthInsta(string key);

    }
}