using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Model;

public class DiscordOptions
{
    [Required]
    public required string ClientId { get; set; }
    [Required]
    public required string ClientSecret { get; set; }
    [Required]
    public required string UserId { get; set; }
}