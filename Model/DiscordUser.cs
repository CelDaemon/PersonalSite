using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Web;
using PersonalSite.Services;

namespace PersonalSite.Model;
public readonly record struct DiscordUser(
    [property: JsonRequired] string Id,
    [property: Required] string Username,
    [property: Required] string Discriminator,
    string? GlobalName,
    [property: JsonPropertyName("avatar")] string? AvatarId,
    bool Bot,
    bool System,
    bool MfaEnabled,
    [property: JsonPropertyName("banner")] string? BannerId,
    int? AccentColor,
    string? Locale,
    string? Email,
    bool? Verified,
    DiscordFlag Flags,
    DiscordPremiumType PremiumType,
    DiscordFlag PublicFlags,
    [property: JsonPropertyName("avatar_decoration")]
    string? AvatarDecorationId)
{
    public Uri GetAvatar(int size = 256)
    {
        if (AvatarId == null)
        {
            int id;
            if (Discriminator == "0")
            {
                var userId = Convert.ToInt64(Id);
                id = (int) (userId >> 22) % 6;
            }
            else
            {
                var discriminatorNum = Convert.ToUInt16(Discriminator);
                id = discriminatorNum % 5;
            }

            return new Uri(DiscordService.CdnBase, $"embed/avatars/{id}.png");
        }
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["size"] = size.ToString();
        var builder = new UriBuilder(new Uri(DiscordService.CdnBase, $"avatars/{Id}/{AvatarId}"))
        {
            Query = query.ToString()
        };
        return builder.Uri;
    }

    [JsonIgnore]
    public Uri? Banner => BannerId != null ? new Uri($"https://cdn.discordapp.com/banners/{Id}/{BannerId}.png") : null;

    [JsonIgnore]
    public Uri? AvatarDecoration => AvatarDecorationId != null
        ? new Uri($"https://cdn.discordapp.com/avatar-decorations/{Id}/{AvatarDecorationId}.png")
        : null;
}