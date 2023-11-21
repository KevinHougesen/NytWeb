using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Data;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NytWeb.Models;


public class PostModel
{
    [Key]
    [JsonProperty(PropertyName = "id")]
    public required string Id { get; set; }

    public string? DisplayName { get; set; }

    public string? Location { get; set; }

    public string Description { get; set; }

    public string? PicturePath { get; set; }
    public string? UserPicturePath { get; set; }

    public ICollection<LikesModel>? Likes { get; set; } = new Collection<LikesModel>();

    //public List<string> Comments { get; set; } = new List<string>();

    public DateTime Time { get; set; }

    [JsonProperty(PropertyName = "UserId")]
    [ForeignKey("UserId")]
    public string? UserId { get; set; }
    public virtual UserModel? User { get; set; }



}

