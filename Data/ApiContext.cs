using Microsoft.EntityFrameworkCore;
using NytWeb.Models;
using Microsoft.Azure.Cosmos;

namespace NytWeb.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        public DbSet<FollowModel> Follows { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {


        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            // Create a CosmosDB client.
            CosmosClient client = new CosmosClient("AccountEndpoint=https://nytapp-db.documents.azure.com:443/;AccountKey=JQ7KtPNPPgXRnvUR7F2VJes0BGEjRHmqGiHaADL11K4jib1cnuAEhO5UWbiraKSdTXmYBt8az49FACDbncbT1A==;");

            // Get the database and collection.
            Database database = client.GetDatabase("Social");
            Container container = database.GetContainer("User");

            var feedIterator = container.GetItemQueryIterator<UserModel>($"SELECT * FROM c");

            List<UserModel> itemList = new();

            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync())
                {
                    itemList.Add(item);
                }
            }


            // Close the client.
            client.Dispose();


            return itemList;
        }

        public async Task CreateUserAsync(UserModel NewUser)
        {
            // Create a CosmosDB client.
            CosmosClient client = new CosmosClient("AccountEndpoint=https://nytapp-db.documents.azure.com:443/;AccountKey=JQ7KtPNPPgXRnvUR7F2VJes0BGEjRHmqGiHaADL11K4jib1cnuAEhO5UWbiraKSdTXmYBt8az49FACDbncbT1A==;");

            // Get the database and collection.
            Database database = client.GetDatabase("Social");
            Container container = database.GetContainer("User");

            // Update the document.
            await container.CreateItemAsync(NewUser);

            // Close the client.
            client.Dispose();
        }

        public async Task<UserModel>? GetUserAsync(string User)
        {
            // Create a CosmosDB client.
            CosmosClient client = new CosmosClient("AccountEndpoint=https://nytapp-db.documents.azure.com:443/;AccountKey=JQ7KtPNPPgXRnvUR7F2VJes0BGEjRHmqGiHaADL11K4jib1cnuAEhO5UWbiraKSdTXmYBt8az49FACDbncbT1A==;");

            // Get the database and collection.
            Database database = client.GetDatabase("Social");
            Container container = database.GetContainer("User");

            var user = await container.ReadItemAsync<UserModel>(User, PartitionKey.None);

            // Close the client.
            client.Dispose();

            return user;
        }


    }
}



