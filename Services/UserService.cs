using System.Text;
using Newtonsoft.Json;
using NytWeb.Models;

namespace NytWeb.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        private readonly string Context;
        private readonly string Key;

        public UserService(HttpClient client)
        {
            _client = client;
            var (context, key) = NytWeb.Config.UnpackContextConfig();
            Context = context;
            Key = key;
        }

        // Get Singular User
        public async Task<UserModel> GetUserAsync(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetUserAsync" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserModel>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        // Get All Users
        public async Task<List<UserModel>> GetUsersAsync()
        {
            // CREATING URL STRING
            string apiURL = Context + "GetUserAsync" + Key;

            // SENDING JSON CONTENT
            var response = await _client.GetAsync(apiURL);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<UserModel>>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<List<string>> GetUserCommunitiesAsync(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetUserCommunitiesAsync" + Key;
            string url = "http://localhost:7071/api/GetUserCommunitiesAsync";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<string>>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        // Get All Users
        public async Task<List<string>> GetUserFollowersAsync(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetUserFollowersAsync" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<string>>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        // Get All Users
        public async Task<List<string>> GetUserFollowingAsync(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetUserFollowingAsync" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<string>>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        // Get All Users
        public async Task<bool> FollowUserAsync(string Username, string ToFollow)
        {
            // CREATING URL STRING
            string apiURL = Context + "FollowUserAsync" + Key;
            string url = "http://localhost:7071/api/FollowUserAsync";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username, ToFollow });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<bool>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

    }
}