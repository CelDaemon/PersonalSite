using Microsoft.AspNetCore.Mvc;

namespace PersonalSite.Util;

public static class UrlHelperExtensions
{
    public static Uri ContentLink(this IUrlHelper helper, HttpContext httpContext, string path)
    {
        return new Uri(new Uri($"{httpContext.Request.Scheme}://{httpContext.Request.Host}"), helper.Content(path));
    }
}