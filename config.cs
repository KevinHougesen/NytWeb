namespace NytWeb
{
    public static class Config
    {
        private static readonly string Connection;
        private static readonly string Key;

        private static readonly string BlobKey;

        private static readonly string JwtSecret;

        private static readonly int SaltRounds;

        static Config()
        {


            JwtSecret = Environment.GetEnvironmentVariable("JwtSecret");

            Connection = Environment.GetEnvironmentVariable("CONTEXT");
            Key = Environment.GetEnvironmentVariable("CONTEXT_KEY");

            BlobKey = Environment.GetEnvironmentVariable("AzureBlobStorage");

            SaltRounds = int.Parse(Environment.GetEnvironmentVariable("PasswordRounds"));
        }

        public static string UnpackJwtConfig()
        {
            return JwtSecret;
        }

        public static (string Connection, string Key) UnpackContextConfig()
        {
            return (Connection, Key);
        }

        public static string UnpackBlobConfig()
        {
            return BlobKey;
        }

        public static int UnpackPasswordConfig()
        {
            return SaltRounds;
        }
    }
}
