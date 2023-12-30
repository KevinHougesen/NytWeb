using NytWeb.Models;

namespace NytWeb.Services;

public interface IPostService
{
    Task<List<PostDetails>> DisplayFeedAsync(string Username);
    Task<List<PostDetails>> GetPostRepliesAsync(string postId);
    Task<List<PostDetails>> GetRootPostRepliesAsync(string postId);
    Task<List<string>> GetGroupsForPostNotification(string PostId);

    Task<string> CreatePostAsync(string Username, string Content, string? BlobUrl);

    Task<string> CreateReplyAsync(string Username, string TargetId, string Content, string Picture);
    Task<PostModel> GetPostMByIdAsync(string postId);
    Task<PostDetails> GetPostByIdAsync(string postId);

    Task<PostModel> CreateQuoteAsync(string Username, string TargetId, string Content);
    Task<bool> OnLikePost(string Username, string PostId);

    Task<bool> OnSharePost(string Username, string PostId);

    Task<int> GetLikesCount(string Username);

    Task<int> GetSharesCount(string Username);

    Task<float> UpdateUserEmbedding(string Username, string TargetId);

    Task<string> DeletePostByIdAsync(string TargetId);
}
