using SocialMediaLinks.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.Configure<SocialMediaLinksOptions>(builder.Configuration.GetSection("SocialMediaLinks"));

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
