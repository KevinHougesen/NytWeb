using NytWeb.Models;

namespace NytWeb.Services
{
    public interface IAuthService
    {
        Task<UserModel> RegisterAsync(UserModel User);

        Task<string> DeleteUserByIdAsync(string Username);

        Task<string> DeleteUserAsync(string Username);

        Task<string> ConnectToHub(string Username);

    }
}