namespace NytWeb.Models;

public class Post
{
    public string Id { get; set; }
    public string Content { get; set; }
    public string? PicturePath { get; set; }
    public string? VideoPath { get; set; }
    public string? AudioPath { get; set; }
}

public class PostDetails
{
    public PostModel Post { get; set; }
    public string PostCreatorUsername { get; set; }
    public string PostCreatorDisplayName { get; set; }
    public List<string> LikedBy { get; set; }
    public List<string> RepostedBy { get; set; }
    public int LikesCount { get; set; }
    public int RepostsCount { get; set; }
    public int RepliesCount { get; set; }
    public string PostType { get; set; }
}
