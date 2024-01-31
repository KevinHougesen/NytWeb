using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NytWeb.Models;
public class InstagramMedia
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("caption")]
    public string Caption { get; set; }
}
