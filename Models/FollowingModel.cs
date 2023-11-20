using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace NytWeb.Models;

public class FollowingModel
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "FollowingId")]
    [ForeignKey("FollowingId")]
    public string? FollowingId { get; set; }

}