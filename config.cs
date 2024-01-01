using Microsoft.Extensions.Configuration;

namespace NytWeb.Configuration
{
    public interface IConfig
    {
        string UnpackJwtConfig();
        (string Connection, string Key) UnpackContextConfig();
        string UnpackBlobConfig();
        int UnpackPasswordConfig();
    }
    public class Config : IConfig
    {
        private readonly IConfiguration _configuration;
        private readonly string Connection;
        private readonly string Key;

        private readonly string BlobKey;

        private readonly string JwtSecret;

        private readonly int SaltRounds;

        public Config(IConfiguration configuration)
        {
            _configuration = configuration;

            JwtSecret = _configuration.GetValue<string>("JwtSecret");

            Connection = _configuration.GetValue<string>("CONTEXT");
            Key = _configuration.GetValue<string>("CONTEXT_KEY");

            BlobKey = _configuration.GetValue<string>("AzureBlobStorage");

            SaltRounds = 10;
        }

        public string UnpackJwtConfig()
        {
            return JwtSecret;
        }

        public (string Connection, string Key) UnpackContextConfig()
        {
            return (Connection, Key);
        }

        public string UnpackBlobConfig()
        {
            return BlobKey;
        }

        public int UnpackPasswordConfig()
        {
            return SaltRounds;
        }
    }
}
