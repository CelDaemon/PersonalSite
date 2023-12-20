using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Model;

public readonly record struct OAuthToken([property:Required] string TokenType, 
    [property:Required] string AccessToken, 
    [property:Required] int ExpiresIn, 
    [property:Required] string RefreshToken, 
    [property:Required] string Scope);