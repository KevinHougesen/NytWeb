using NytWeb.Data;
using NytWeb.Models;

namespace NytWeb.Algorithms
{


    public class ContentRanker
    {
        private readonly ApiContext _context;

        // Database Constructor
        public ContentRanker(ApiContext context)
        {
            _context = context;
        }

        public async Task<List<PostModel>> RankByLikes(List<PostModel> posts)
        {
            var rankedPosts = posts.OrderByDescending(p => p.Likes.Count).ToList();
            return rankedPosts;
        }
    }
};