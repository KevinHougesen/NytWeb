using System.Text;
using Newtonsoft.Json;
using NytWeb.Configuration;
using NytWeb.Models;

namespace NytWeb.Services
{

    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;
        private readonly string Context;
        private readonly IConfig _config;
        private readonly string Key;
        private readonly string INSTAGRAM_APP_ID;
        private readonly string INSTAGRAM_APP_SECRET;
        private readonly string INSTAGRAM_REDIRECT_URI;

        public AuthService(HttpClient client, IConfig config)
        {
            _client = client;
            _config = config;
            var (context, key) = _config.UnpackContextConfig();
            INSTAGRAM_APP_ID = "1320174152716541";
            INSTAGRAM_APP_SECRET = "d92d0eedd459439af59da6876f164aa6";
            INSTAGRAM_REDIRECT_URI = "https://www.ditnyt.dk/";
            Context = context;
            Key = key;
        }


        // Get All Users
        public async Task<UserModel> RegisterAsync(UserModel User)
        {
            // CREATING URL STRING
            string apiURL = Context + "RegisterAsync" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { User });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserModel>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<string> AuthInsta(string key)
        {
            // CREATING URL STRING
            string apiURL = "https://api.instagram.com/" + "oauth/access_token";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { client_id = INSTAGRAM_APP_ID, client_secret = INSTAGRAM_APP_SECRET, grant_type = "authorization_code", redirect_uri = INSTAGRAM_REDIRECT_URI, code = key });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO STRING
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            // RETURNING FINAL RESULT
            return result;
        }


        // Get Singular User
        public async Task<string> DeleteUserByIdAsync(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "DeleteUserByIdAsync" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<string>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<string> ConnectToHub(string Username, List<string> groupName)
        {
            // SENDING JSON CONTENT
            var response = await _client.GetStringAsync($"http://localhost:5022/negotiate?id={Username}&firstGroup={groupName[0]}&secondGroup={groupName[1]}&thirdGroup={groupName[2]}");

            // RETURNING FINAL RESULT
            return response;
        }

        // Get Singular User
        public async Task<string> DeleteUserAsync(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "DeleteUserAsync" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<string>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

    }
}