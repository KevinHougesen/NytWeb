using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NytWeb.Models;


public class UserModel
{
    [Key]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [Required]
    public string Username { get; set; }

    // public string UserName { get; set; }

    [Required]
    public string DisplayName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    // [Required]
    // [Compare(nameof(Password))]
    // public string Password2 { get; set; }
    public string? ProfilePicturePath { get; set; }
    public string? EmailVerificationToken { get; set; }
    public DateTime? EmailVerifiedAt { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public string? UserVerifiedAt { get; set; }
    public DateTime? UserVerified { get; set; }
    public string Role { get; set; }

    public string? Location { get; set; }

    public string? Occupation { get; set; }

    public int? ViewedProfile { get; set; }

    // public int Impressions { get; set; }

    public UserModel() { }

}
