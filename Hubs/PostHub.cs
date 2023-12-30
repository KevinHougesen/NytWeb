using Microsoft.AspNetCore.SignalR;
using NytWeb.Services;

namespace BlazorSignalRApp.Hubs;

public class PostHub(IPostService postService) : Hub
{
    private readonly IPostService _postService = postService;

    public async Task NotifyNewPost(string PostId)
    {
        try
        {
            var postId = PostId.Replace("\"", "");
            //var post = await _postService.GetPostByIdAsync(PostId);

            var com = await _postService.GetGroupsForPostNotification(postId);
            var community = com.FirstOrDefault();

            Console.WriteLine(postId + " lol");
            // First element is the community, the rest are users
            Console.WriteLine(community);

            // Notify the community group
            if (!string.IsNullOrEmpty(community))
            {
                Console.WriteLine(community + "LOL");
                await Clients.All.SendAsync("NewPostAvailable", postId);
            }

            // Notify individual users
            /*
            foreach (var user in users)
            {
                Console.WriteLine(user);
                await Clients.User(user).SendAsync("NewPostAvailable", PostId);
            }
            */
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task NotifyPostLiked(string postId, bool liked)
    {
        // Broadcast to all connected clients
        await Clients.Others.SendAsync("PostLiked", postId, liked);
    }

    public async Task NotifyPostReposted(string postId, bool repost)
    {
        // Broadcast to all connected clients
        await Clients.Others.SendAsync("PostReposted", postId, repost);
    }

    public async Task NotifyNewFollower(string userId, bool follow)
    {
        // Broadcast to all connected clients
        await Clients.Others.SendAsync("NewFollower", userId, follow);
    }

    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

}