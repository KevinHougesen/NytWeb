using System.Text;
using Newtonsoft.Json;
using NytWeb.Configuration;
using NytWeb.Models;

namespace NytWeb.Services
{
    public class PostService : IPostService
    {
        private readonly HttpClient _client;
        private readonly IConfig _config;
        private readonly string Context;
        private readonly string Key;

        public PostService(HttpClient client, IConfig config)
        {
            _client = client;
            _config = config;
            var (context, key) = _config.UnpackContextConfig();
            Context = context;
            Key = key;
        }

        public async Task<List<PostDetails>> DisplayFeedAsync(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "DisplayUsersFeed" + Key;
            string url = "http://localhost:7071/api/DisplayUsersFeed";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PostDetails>>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<int> UpdatePostViewAsync(string userId, string postId, int viewTimeInSeconds)
        {
            // CREATING URL STRING
            string apiURL = Context + "DisplayUsersFeed" + Key;
            string url = "http://localhost:7071/api/UpdatePostViewAsync";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { userId, postId, viewTimeInSeconds });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(url, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<List<string>> GetGroupsForPostNotification(string PostId)
        {
            string apiURL = Context + "GetGroupsForPostNotification" + Key;
            string url = "http://localhost:7071/api/GetGroupsForPostNotification?PostId=";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { PostId });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.GetAsync(apiURL + PostId);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<string>>(jsonString);
            Console.WriteLine(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<string> CreatePostAsync(string Username, string Content, string? BlobUrl)
        {
            // CREATING URL STRING
            string apiURL = Context + "CreatePostAsync" + Key;
            string url = "http://localhost:7071/api/CreatePostAsync";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username, Content, BlobUrl });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var result = await response.Content.ReadAsStringAsync();

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<string> CreateReplyAsync(string Username, string TargetId, string Content, string Picture)
        {
            // CREATING URL STRING
            string apiURL = Context + "CreateReplyAsync" + Key;
            string uri = "http://localhost:7071/api/CreateReplyAsync";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username, TargetId, Content, Picture });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<string>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<PostModel> CreateQuoteAsync(string Username, string TargetId, string Content)
        {
            // CREATING URL STRING
            string apiURL = Context + "CreateQuoteAsync" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username, TargetId, Content });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PostModel>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<bool> OnSharePost(string Username, string PostId)
        {
            // CREATING URL STRING
            string apiURL = Context + "OnSharePost" + Key;
            string url = "http://localhost:7071/api/OnSharePost";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username, PostId });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<bool>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<bool> OnLikePost(string Username, string PostId)
        {
            // CREATING URL STRING
            string apiURL = Context + "OnLikePost" + Key;
            string url = "http://localhost:7071/api/OnLikePost";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username, PostId });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            var result = JsonConvert.DeserializeObject<bool>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<PostDetails> GetPostByIdAsync(string postId)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetPostByIdAsync" + Key;
            string url = "http://localhost:7071/api/GetPostByIdAsync";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { postId });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PostDetails>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<List<PostDetails>> GetRootPostRepliesAsync(string postId)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetRootPostRepliesAsync" + Key;
            string url = "http://localhost:7071/api/GetRootPostRepliesAsync";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { postId });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PostDetails>>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<List<PostDetails>> GetPostRepliesAsync(string postId)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetPostRepliesAsync" + Key;
            string url = "http://localhost:7071/api/GetPostRepliesAsync";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { postId });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PostDetails>>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<PostModel> GetPostMByIdAsync(string postId)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetPostByIdAsync" + Key;
            string url = "http://localhost:7071/api/GetPostMByIdAsync";

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { postId });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PostModel>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<int> GetLikesCount(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetLikesCount" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(jsonString);

            // RETURNING FINAL
            return result;
        }

        public async Task<int> GetSharesCount(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "GetSharesCount" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(jsonString);

            // RETURNING FINAL
            return result;
        }

        public async Task<float> UpdateUserEmbedding(string Username, string TargetId)
        {
            // CREATING URL STRING
            string apiURL = Context + "UpdateUserEmbedding" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username, TargetId });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<float>(jsonString);

            // RETURNING FINAL
            return result;
        }

        public async Task<string> DeletePostByIdAsync(string TargetId)
        {
            // CREATING URL STRING
            string apiURL = Context + "DisplayUsersFeed" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { TargetId });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<string>(jsonString);

            // RETURNING FINAL
            return result;
        }

    }
}