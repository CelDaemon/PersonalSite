using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PersonalSite.Services;
using PersonalSite.Util;

namespace PersonalSite.Controllers;

[Route("discord")]
public class DiscordProfileController(DiscordService discordService) : Controller
{
    [HttpGet("redirect")]
    public IActionResult Get()
    {
        return Redirect(discordService.GetRedirectUri().ToString());
    }
    [HttpGet("login")]
    [ValidateInput]
    public IActionResult Token([BindRequired, FromQuery(Name = "code")] string code)
    {
        return Ok();
    }
}