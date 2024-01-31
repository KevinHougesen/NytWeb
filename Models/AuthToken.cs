using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NytWeb.Models;
public class InstagramAuthResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("user_id")]
    public long UserId { get; set; }
}
