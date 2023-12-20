using Microsoft.AspNetCore.HttpOverrides;
using PersonalSite.Model;
using PersonalSite.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = false);
builder.Services.AddOptions<DiscordOptions>().BindConfiguration("Discord").ValidateDataAnnotations();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<DiscordService>();
var app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
app.UseStaticFiles();

app.MapControllers();
app.MapRazorPages();

app.Run();