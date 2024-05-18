using System.Text;
using Newtonsoft.Json;
using NytWeb.Configuration;
using NytWeb.Models;

namespace NytWeb.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        private readonly IConfig _config;
        private readonly string Context;
        private readonly string Key;

        public UserService(HttpClient client, IConfig config)
        {
            _client = client;
            _config = config;
            var (context, key) = _config.UnpackContextConfig();
            Context = context;
            Key = key;
        }

        // Get Singular User
        public async Task<UserModel> GetUserAsync(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetUserAsync" + Key;
            string url = "http://localhost:7071/api/GetUserAsync";

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

        public async Task<InstagramAuthResponse> GetUserInstaTokenAsync(string code)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetUserAsync" + Key;
            string url = "https://api.instagram.com/oauth/access_token";
            var authLinkUri = new Uri(@"https://api.instagram.com/oauth/access_token");

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { client_id = "1320174152716541", client_secret = "d92d0eedd459439af59da6876f164aa6", grant_type = "authorization_code", redirect_uri = "https://localhost:7174/profile/", code });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");

            var postValues = new List<KeyValuePair<string, string>>
                             {
                                 new KeyValuePair<string, string>
                                     ("client_id",
                                      "1320174152716541"),
                                 new KeyValuePair<string, string>
                                     ("client_secret",
                                      "d92d0eedd459439af59da6876f164aa6"),
                                 new KeyValuePair<string, string>
                                     ("grant_type",
                                      "authorization_code"),
                                 new KeyValuePair<string, string>
                                     ("redirect_uri",
                                      "https://localhost:7174/profile/"),
                                 new KeyValuePair<string, string>("code", code)
                             };

            var fields = new Dictionary<string, string>
            {
                {"client_id", "1320174152716541"},
                {"client_secret", "d92d0eedd459439af59da6876f164aa6"},
                {"grant_type", "authorization_code"},
                {"redirect_uri", "https://localhost:7174/profile/"},
                {"code", code}
            };
            var content = new FormUrlEncodedContent(fields);

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(authLinkUri, content);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<InstagramAuthResponse>(jsonString);


            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<IgLongToken> GetUserInstaLongTokenAsync(string token)
        {

            // SENDING JSON CONTENT
            var response = await _client.GetAsync($"https://graph.instagram.com/access_token?grant_type=ig_exchange_token&client_secret=d92d0eedd459439af59da6876f164aa6&access_token={token}");

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("================================LONGTOKEN================================");
            Console.WriteLine(jsonString);
            Console.WriteLine("================================LONGTOKEN================================");
            var igLongToken = JsonConvert.DeserializeObject<IgLongToken>(jsonString);

            // RETURNING FINAL RESULT
            return igLongToken;
        }

        public async Task<InstagramMediaResponse> GetUserInstaMediaAsync(string token)
        {

            // SENDING JSON CONTENT
            var response = await _client.GetAsync($"https://graph.instagram.com/me/media?fields=id,caption&access_token={token}");

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<InstagramMediaResponse>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<InstagramMediaData> GetUserInstaMediaDataAsync(string id, string token)
        {

            // SENDING JSON CONTENT
            var response = await _client.GetAsync($"https://graph.instagram.com/{id}?fields=id,media_type,media_url,permalink,username,timestamp&access_token={token}");

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("================================MEDIA DATA================================");
            Console.WriteLine(jsonString);
            Console.WriteLine("================================MEDIA DATA================================");

            var result = JsonConvert.DeserializeObject<InstagramMediaData>(jsonString);


            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<InstagramMediaResponse> GetUserInstaMediaAlbumChildrenAsync(string id, string token)
        {
            // SENDING JSON CONTENT
            var response = await _client.GetAsync($"https://graph.instagram.com/{id}/children?&access_token={token}");

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("================================ALBUM DATA================================");
            Console.WriteLine(jsonString);
            Console.WriteLine("================================ALBUM DATA================================");

            var result = JsonConvert.DeserializeObject<InstagramMediaResponse>(jsonString);


            // RETURNING FINAL RESULT
            return result;
        }

        public bool VerifyPass(string Password, string userAccountPassword)
        {
            var isMatch = BCrypt.Net.BCrypt.EnhancedVerify(Password, userAccountPassword);
            return isMatch;
        }

        // Get All Users
        public async Task<List<UserModel>> GetUsersAsync()
        {
            // CREATING URL STRING
            string apiURL = Context + "GetUsersAsync" + Key;

            // SENDING JSON CONTENT
            var response = await _client.GetAsync(apiURL);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<UserModel>>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<string> UpdateInstaTokenAsync(string Username, string Token)
        {
            // CREATING URL STRING
            string apiURL = Context + "UpdateInstaTokenAsync" + Key;
            string url = $"http://localhost:7071/api/UpdateInstaTokenAsync";

            var payload = JsonConvert.SerializeObject(new { Username, Token });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var result = await response.Content.ReadAsStringAsync();

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
            var response = await _client.PostAsync(url, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            var result = JsonConvert.DeserializeObject<List<string>>(jsonString);

            // CHECKING IF RESULT IS EMPTY
            if (result == null || !result.Any())
            {
                result = new List<string> { "Politik", "Underholdning", "Mode" };
            }

            // If the result contains less than 3 different communities, randomly add communities from the provided list until there are 3 different communities
            var random = new Random();
            var availableCommunities = new List<string> { "Sport", "Konflikt", "Mode", "Musik", "Underholdning", "Mad", "Videnskab", "Sundhed", "Samfund", "Politik" };
            var distinctCommunities = result.Distinct().ToList();
            while (distinctCommunities.Count < 3)
            {
                var randomCommunity = availableCommunities[random.Next(availableCommunities.Count)];
                if (!distinctCommunities.Contains(randomCommunity))
                {
                    distinctCommunities.Add(randomCommunity);
                }
            }

            // RETURNING FINAL RESULT
            return distinctCommunities;
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

        public async Task<List<string>> GetUserSeenPostAsync(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetUserSeenPostAsync" + Key;
            string url = "http://localhost:7071/api/GetUserSeenPostAsync";

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