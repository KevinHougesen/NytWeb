using System.Text;
using Newtonsoft.Json;
using NytWeb.Models;

namespace NytWeb.Services
{

    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;
        private readonly string Context;
        private readonly string Key;

        public AuthService(HttpClient client)
        {
            _client = client;
            var (context, key) = NytWeb.Config.UnpackContextConfig();
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