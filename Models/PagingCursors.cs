using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NytWeb.Models;
public class PagingCursors
{
    [JsonProperty("after")]
    public string After { get; set; }

    [JsonProperty("before")]
    public string Before { get; set; }
}
