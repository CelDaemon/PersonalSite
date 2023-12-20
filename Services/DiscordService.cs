using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using PersonalSite.Model;

namespace PersonalSite.Services;

public class DiscordService(IOptions<DiscordOptions> options, HttpClient client, ILogger<DiscordService> logger)
{
    public static readonly Uri ApiBase = new("https://discord.com/api/v10");
    public static readonly Uri CdnBase = new("https://cdn.discordapp.com");
    private OAuthToken? _cachedToken;
    private readonly SemaphoreSlim _cachedTokenLock = new(1, 1);

    private DiscordUser? _cachedUser;
    private DateTime? _cachedUserTimestamp;
    private readonly SemaphoreSlim _cachedUserLock = new(1, 1);
    
    public async Task<DiscordUser> GetUserAsync()
    {
        try
        {
            await _cachedUserLock.WaitAsync();
            if (_cachedUser != null && _cachedUserTimestamp != null &&
                DateTime.UtcNow < _cachedUserTimestamp.Value.AddHours(1)) return _cachedUser.Value;
            var token = await GetToken(false);
            using var userRequest = new HttpRequestMessage(HttpMethod.Get, new Uri(ApiBase, $"users/@me"));
            userRequest.Headers.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);
            using var userResponse = await client.SendAsync(userRequest);
            userResponse.EnsureSuccessStatusCode();
            var user = await JsonSerializer.DeserializeAsync(await userResponse.Content.ReadAsStreamAsync(),
                SourceGenerationContext.Default.DiscordUser);
            _cachedUser = user;
            _cachedUserTimestamp = DateTime.UtcNow;
            return user;
        }
        finally
        {
            _cachedUserLock.Release();
        }
    }

    private async Task<OAuthToken> GetToken(bool forceRefresh)
    {
        try
        {
            await _cachedTokenLock.WaitAsync();
            if (!forceRefresh && _cachedToken != null) return _cachedToken.Value;
            using var tokenRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(ApiBase, "oauth2/token"));
            tokenRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["scope"] = "identify email",
                ["client_id"] = options.Value.ClientId,
                ["client_secret"] = options.Value.ClientSecret
            });
            using var tokenResponse = await client.SendAsync(tokenRequest);
            tokenResponse.EnsureSuccessStatusCode();
            var token =  JsonSerializer.Deserialize(await tokenResponse.Content.ReadAsStreamAsync(), SourceGenerationContext.Default.OAuthToken);
            _cachedToken = token;
            return token;
        }
        finally
        {
            _cachedTokenLock.Release();
        }
    }
}