using NytWeb.Models;

namespace NytWeb.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient client = new HttpClient();

        // Get Singular User
        public async Task<UserModel> GetUserAsync(string userId)
        {
            Console.WriteLine("fetching");
            UserModel response = await client.GetFromJsonAsync<UserModel>($"http://localhost:7071/api/GetUserAsync?={userId}");
            return response;
        }

        // Get All Users
        public async Task<List<UserModel>> GetUsersAsync()
        {
            Console.WriteLine("fetching");
            List<UserModel> response = await client.GetFromJsonAsync<List<UserModel>>($"http://localhost:7071/api/GetUsersAsync");
            return response;
        }

        // Get All Users
        public async Task<List<string>> GetUserFollowersAsync(string userId)
        {
            Console.WriteLine("fetching");
            List<string> response = await client.GetFromJsonAsync<List<string>>($"http://localhost:7071/api/GetUserFollowersAsync?={userId}");
            return response;
        }

        // Get All Users
        public async Task<List<string>> GetUserFollowingAsync(string userId)
        {
            Console.WriteLine("fetching");
            List<string> response = await client.GetFromJsonAsync<List<string>>($"http://localhost:7071/api/GetUserFollowingAsync?={userId}");
            return response;
        }

        // Get All Users
        public async Task<List<UserModel>> FollowUserAsync(string userId, string toFollow)
        {
            Console.WriteLine("fetching");
            List<UserModel> response = await client.GetFromJsonAsync<List<UserModel>>($"http://localhost:7071/api/GetUserFollowingAsync?={userId}&={toFollow}");
            return response;
        }

    }
}