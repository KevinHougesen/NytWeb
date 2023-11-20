using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Data;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NytWeb.Models;

public class LikesModel
{
    [Key]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public string? UserId { get; set; }
    public string? PostId { get; set; }

}

