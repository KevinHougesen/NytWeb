using NytWeb.Models;

namespace NytWeb.Services
{

    public class AuthService : IAuthService
    {
        static readonly HttpClient client = new HttpClient();

        // Get All Users
        public async Task<UserModel> RegisterAsync(UserModel User)
        {
            Console.WriteLine("fetching");
            var response = await client.GetAsync($"http://localhost:7071/api/RegisterAsync?={User}");
            var content = await response.Content.ReadFromJsonAsync<UserModel>();
            return content;
        }


        // Get Singular User
        public async Task<UserModel> DeleteUserByIdAsync(string userId)
        {
            Console.WriteLine("fetching");
            var response = await client.GetAsync($"http://localhost:7071/api/DeleteUserByIdAsync?={userId}");
            var content = await response.Content.ReadFromJsonAsync<UserModel>();
            return content;
        }

        // Get Singular User
        public async Task<UserModel> DeleteUserAsync(string userId)
        {
            Console.WriteLine("fetching");
            var response = await client.GetAsync($"http://localhost:7071/api/DeleteUserAsync?={userId}");
            var content = await response.Content.ReadFromJsonAsync<UserModel>();
            return content;
        }

    }
}