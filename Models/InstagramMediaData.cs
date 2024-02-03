using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NytWeb.Models;
public class InstagramMediaData
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("media_type")]
    public string MediaType { get; set; }
    [JsonProperty("media_url")]
    public string MediaUrl { get; set; }
    [JsonProperty("permalink")]
    public string Permalink { get; set; }
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }
}
