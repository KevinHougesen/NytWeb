using Newtonsoft.Json;

namespace NytWeb.Models;
public class IgLongToken
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }

    [JsonProperty("expires_in")]
    public long ExpiresIn { get; set; }
}
