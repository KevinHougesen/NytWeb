using NytWeb.Algorithms;
using NytWeb.Data;
using NytWeb.Services;
using NytWeb.Models;

namespace NytWeb.Algorithms
{
    public class ContentFilter
    {
        public async Task<List<PostModel>> FilterByInterestsAsync(List<PostModel> posts, string occupation)
        {
            var filteredPosts = new List<PostModel>();
            foreach (var post in posts)
            {
                if (post.Description.Contains(occupation))
                {
                    filteredPosts.Add(post);
                }
            }
            return filteredPosts;
        }
    }
}