using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace NytWeb.Models;

[PrimaryKey("id")]
public class UserPostsModel
{
    [Key]
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }

    [JsonProperty(PropertyName = "PostId")]
    [ForeignKey("PostId")]
    public string? PostId { get; set; }
    public int LikesCount { get; set; }

}