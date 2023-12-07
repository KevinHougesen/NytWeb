using System.Net.Http.Json;
using NytWeb.Models;

namespace NytWeb.Services
{
    public class PostService : IPostService
    {
        private readonly HttpClient _client;

        public PostService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<PostModel>> DisplayFeedAsync(string userId)
        {
            return await _client.GetFromJsonAsync<List<PostModel>>($"DisplayUsersFeed?={userId}");
        }

        public async Task<PostModel> CreatePostAsync(string UserId, string Content)
        {
            return await _client.GetFromJsonAsync<PostModel>($"CreatePostAsync?UserId={UserId}&Content={Content}");
        }

        public async Task<PostModel> CreateReplyAsync(string UserId, string TargetId, string Content)
        {
            return await _client.GetFromJsonAsync<PostModel>($"CreateReplyAsync?={UserId}&={TargetId}&={Content}");
        }

        public async Task<PostModel> CreateQuoteAsync(string UserId, string TargetId, string Content)
        {
            return await _client.GetFromJsonAsync<PostModel>($"CreateQuoteAsync?={UserId}&={TargetId}&={Content}");
        }

        public async Task<PostModel> OnSharePost(string UserId, string TargetId)
        {
            return await _client.GetFromJsonAsync<PostModel>($"OnSharePost?={UserId}&={TargetId}");
        }

        public async Task<string> OnLikePost(string UserId, string TargetId)
        {
            return await _client.GetFromJsonAsync<string>($"OnLikePost?={UserId}&={TargetId}");
        }

        public async Task<PostModel> GetPostByIdAsync(string UserId)
        {
            return await _client.GetFromJsonAsync<PostModel>($"GetPostByIdAsync?={UserId}");
        }

        public async Task<int> GetLikesCount(string UserId)
        {
            return await _client.GetFromJsonAsync<int>($"OnSharePost?={UserId}");
        }

        public async Task<int> GetSharesCount(string UserId)
        {
            return await _client.GetFromJsonAsync<int>($"GetSharesCount?={UserId}");
        }

        public async Task<float> UpdateUserEmbedding(string UserId, string TargetId)
        {
            return await _client.GetFromJsonAsync<float>($"UpdateUserEmbedding?={UserId}&={TargetId}");
        }

        public async Task<string> DeletePostByIdAsync(string TargetId)
        {
            return await _client.GetFromJsonAsync<string>($"DeletePostByIdAsync?={TargetId}");
        }

    }
}