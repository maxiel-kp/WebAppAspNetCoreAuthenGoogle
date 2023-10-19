using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc.RazorPages;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Not set to GoogleDefaults.AuthenticationScheme
//})
//.AddGoogle(options =>
//{
//    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//});

builder.Services.AddAuthentication("Cookies")
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    })
    .AddCookie("Cookies");


builder.Services.AddAuthorization();

// ... Other services ...

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

//app.MapGet("/", (HttpContext context) => context.Response.WriteAsync("Hello, World!"));
app.MapGet("/", (HttpContext context) =>
{
    context.ChallengeAsync(GoogleDefaults.AuthenticationScheme);
    if (context.User.Identity.IsAuthenticated)
    {
        return Results.Redirect("/Privacy");
    }
    else
    {
        return Results.Redirect("/Error");
    }
});

//app.MapGet("/Privacy", (HttpContext context) =>
//{
//    // You can perform any logic here if needed
//    // For now, return a simple response without a Razor Page
//    context.Response.StatusCode = 200;
//    //return Results.Text(context.User.ToString());
//    return Results.Page("/Privacy");
//});



app.Run();
