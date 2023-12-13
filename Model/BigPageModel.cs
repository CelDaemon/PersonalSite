using System.Globalization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalSite.Util;

namespace PersonalSite.Model;

public abstract class BigPageModel : PageModel
{
    public virtual CultureInfo Language => CultureInfo.GetCultureInfo("en");
    public virtual string Username => Anonymous ? "DeVoid" : "Celeste";
    public virtual string? Title => null;
    public virtual Uri CanonicalUri => new(Url.PageLink()!);
    public virtual string Icon => Url.Content(Resources.Icon);
    public virtual string? Description => null;
    public virtual string? EmbedTitle => Title;
    public virtual string? EmbedDescription => Description;
    public virtual Uri EmbedUrl => CanonicalUri;
    
    public virtual Uri EmbedImage => Url.ContentLink(HttpContext, Icon);
    public bool Anonymous { get; private set; }

    public void OnGet([FromQuery(Name = "a")] int a)
    {
        if (HttpContext.Request.Cookies["a"] != "1" && a != 1) return;
        Anonymous = true;
        HttpContext.Response.Cookies.Append("a", "1", new CookieOptions {IsEssential = true, SameSite = SameSiteMode.None, MaxAge = TimeSpan.FromDays(365), Secure = false});
    }
}