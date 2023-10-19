using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configure services
builder.Services.AddAuthentication(GoogleDefaults.AuthenticationScheme)
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    });

// ... Other services ...

// Build the application
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Define endpoints
app.MapGet("/", () => "Hello, World!");

// ... Other endpoints ...

app.Run();
