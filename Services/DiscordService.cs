using System.Web;
using Microsoft.Extensions.Options;
using PersonalSite.Model;

namespace PersonalSite.Services;

public class DiscordService(IOptions<DiscordOptions> options, LinkGenerator linkGenerator, IHttpContextAccessor httpContext) : IOAuthService
{
    public Uri GetRedirectUri()
    {
        
        var query = HttpUtility.ParseQueryString(string.Empty);
        query.Add("client_id", options.Value.ClientId ?? throw new NullReferenceException());
        query.Add("response_type", "code");
        query.Add("redirect_uri", linkGenerator.GetUriByAction(httpContext.HttpContext ?? throw new NullReferenceException(), "Token", "DiscordProfile"));
        query.Add("scope", "identify");
        return new UriBuilder(new Uri("https://discord.com/api/oauth2/authorize"))
        {
            Query = query.ToString()
        }.Uri;
    }
}