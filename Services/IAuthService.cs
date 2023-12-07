using NytWeb.Models;

namespace NytWeb.Services
{
    public interface IAuthService
    {
        Task<UserModel> RegisterAsync(UserModel User);

        Task<UserModel> DeleteUserByIdAsync(string userId);

        Task<UserModel> DeleteUserAsync(string userId);

    }
}