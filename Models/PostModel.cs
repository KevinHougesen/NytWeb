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
    public string? Username { get; set; }


    public string? Location { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }

    public string? PicturePath { get; set; }
    public string? UserPicturePath { get; set; }

}

