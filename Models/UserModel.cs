using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NytWeb.Models;


public class UserModel
{
    [Key]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [Required]
    public string Username { get; set; }

    public string UserName { get; set; }

    [Required]
    public string DisplayName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public string? PicturePath { get; set; }
    public string? EmailVerificationToken { get; set; }
    public DateTime? EmailVerifiedAt { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public string? UserVerifiedAt { get; set; }
    public DateTime? UserVerified { get; set; }
    public string Role { get; set; }

    public virtual ICollection<FollowerModel> Followers { get; set; } = new Collection<FollowerModel>();
    public virtual ICollection<FollowingModel> Following { get; set; } = new Collection<FollowingModel>();

    public string Location { get; set; }

    public string Occupation { get; set; }

    public int ViewedProfile { get; set; }

    public int Impressions { get; set; }

    public ICollection<UserPostsModel> Posts { get; set; } = new Collection<UserPostsModel>();
    public ICollection<LikesModel> Likes { get; set; } = new Collection<LikesModel>();

    public UserModel() { }

}
