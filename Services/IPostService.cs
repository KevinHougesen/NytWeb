using NytWeb.Models;

namespace NytWeb.Services;

public interface IPostService
{
    Task<List<PostModel>> DisplayFeedAsync(string userId);

    Task<PostModel> CreatePostAsync(string UserId, string Content);

    Task<PostModel> CreateReplyAsync(string UserId, string TargetId, string Content);

    Task<PostModel> GetPostByIdAsync(string UserId);

    Task<PostModel> CreateQuoteAsync(string UserId, string TargetId, string Content);

    Task<PostModel> OnSharePost(string UserId, string TargetId);

    Task<int> GetLikesCount(string UserId);

    Task<int> GetSharesCount(string UserId);

    Task<float> UpdateUserEmbedding(string UserId, string TargetId);

    Task<string> DeletePostByIdAsync(string TargetId);
}
