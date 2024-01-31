using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NytWeb.Models;
public class Paging
{
    [JsonProperty("cursors")]
    public PagingCursors Cursors { get; set; }

    [JsonProperty("next")]
    public string Next { get; set; }
}
