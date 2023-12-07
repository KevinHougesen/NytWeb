using NytWeb.Models;

namespace NytWeb.Services;

public interface IPostService
{
    Task<List<PostModel>> DisplayFeedAsync(string Username);

    Task<PostModel> CreatePostAsync(string Username, string Content);

    Task<PostModel> CreateReplyAsync(string Username, string TargetId, string Content);

    Task<PostModel> GetPostByIdAsync(string Username);

    Task<PostModel> CreateQuoteAsync(string Username, string TargetId, string Content);

    Task<PostModel> OnSharePost(string Username, string TargetId);

    Task<int> GetLikesCount(string Username);

    Task<int> GetSharesCount(string Username);

    Task<float> UpdateUserEmbedding(string Username, string TargetId);

    Task<string> DeletePostByIdAsync(string TargetId);
}
