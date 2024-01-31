using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NytWeb.Models;
public class InstagramMediaResponse
{
    [JsonProperty("data")]
    public List<InstagramMedia> Data { get; set; }

    [JsonProperty("paging")]
    public Paging PagingInfo { get; set; }
}
