using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using NytWeb.Models;

namespace NytWeb.Services
{
    public class PostService : IPostService
    {
        private readonly HttpClient _client;
        private readonly string Context;
        private readonly string Key;

        public PostService(HttpClient client)
        {
            _client = _client;
            var (context, key) = NytWeb.Config.UnpackContextConfig();
            Context = context;
            Key = key;
        }

        public async Task<List<PostModel>> DisplayFeedAsync(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "DisplayUsersFeed" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PostModel>>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<PostModel> CreatePostAsync(string Username, string Content)
        {
            // CREATING URL STRING
            string apiURL = Context + "CreatePostAsync" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username, Content });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PostModel>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<PostModel> CreateReplyAsync(string Username, string TargetId, string Content)
        {
            // CREATING URL STRING
            string apiURL = Context + "CreateReplyAsync" + Key;

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

        public async Task<PostModel> OnSharePost(string Username, string TargetId)
        {
            // CREATING URL STRING
            string apiURL = Context + "OnSharePost" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username, TargetId });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PostModel>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<string> OnLikePost(string Username, string TargetId)
        {
            // CREATING URL STRING
            string apiURL = Context + "OnLikePost" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username, TargetId });
            var jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // SENDING JSON CONTENT
            var response = await _client.PostAsync(apiURL, jsonContent);

            // RETURNING REQUEST AND CONVERTING TO OBJECT
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<string>(jsonString);

            // RETURNING FINAL RESULT
            return result;
        }

        public async Task<PostModel> GetPostByIdAsync(string Username)
        {
            // CREATING URL STRING
            string apiURL = Context + "DisplayUsersFeed" + Key;

            // CREATING PAYLOAD AND JSON CONTENT
            var payload = JsonConvert.SerializeObject(new { Username });
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
            string apiURL = Context + "DisplayUsersFeed" + Key;

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
            string apiURL = Context + "DisplayUsersFeed" + Key;

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
            string apiURL = Context + "DisplayUsersFeed" + Key;

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