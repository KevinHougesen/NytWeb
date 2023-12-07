using Microsoft.Extensions.Configuration;

namespace NytWeb
{
    public static class Config
    {
        private static readonly string Neo4jUri;
        private static readonly string Neo4jUsername;
        private static readonly string Neo4jPassword;
        private static readonly string Connection;
        private static readonly string Key;

        private static readonly string JwtSecret;

        private static readonly int SaltRounds;

        static Config()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var neo4j = config.GetSection("Neo4j");
            Neo4jUri = neo4j["uri"];
            Neo4jUsername = neo4j["username"];
            Neo4jPassword = neo4j["password"];

            JwtSecret = config.GetSection("Jwt")["secret"];

            Connection = config.GetSection("ConnectionStrings")["CONTEXT"];
            Key = config.GetSection("ConnectionStrings")["CONTEXT_KEY"];

            SaltRounds = int.Parse(config.GetSection("Password")["rounds"]);
        }

        public static (string Uri, string Username, string Password) UnpackNeo4jConfig()
        {
            return (Neo4jUri, Neo4jUsername, Neo4jPassword);
        }

        public static string UnpackJwtConfig()
        {
            return JwtSecret;
        }

        public static (string Connection, string Key) UnpackContextConfig()
        {
            return (Connection, Key);
        }

        public static int UnpackPasswordConfig()
        {
            return SaltRounds;
        }
    }
}
