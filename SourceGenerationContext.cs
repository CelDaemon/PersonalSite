using System.Globalization;
using System.Text.Json.Serialization;
using PersonalSite.Model;

namespace PersonalSite;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
[JsonSerializable(typeof(OAuthToken))]
[JsonSerializable(typeof(DiscordUser))]
public partial class SourceGenerationContext : JsonSerializerContext
{
    
}