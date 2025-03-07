using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using PhuDHT_NET1717_Spring25FE.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

// Register HttpClient for API communication
builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/"); // Update with your API URL
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true // Allow self-signed certificates
    };
});

builder.Services.AddScoped<HttpClientHandler>();
builder.Services.AddScoped<HttpClient>(sp =>
{
    var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("APIClient");
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
    var token = httpContextAccessor.HttpContext?.Request.Cookies["jwt"];
    if (!string.IsNullOrEmpty(token))
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
    return client;
});
builder.Services.AddHttpClient<ApiService>();


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

// Additional Razor Pages for authentication
app.MapGet("/Account/Login", () => Results.Redirect("/Account/Login"));
app.MapGet("/Account/Register", () => Results.Redirect("/Account/Register"));

// Add Razor Pages for login and register
app.MapRazorPages().WithMetadata(new { PageName = "Account/Login" });
app.MapRazorPages().WithMetadata(new { PageName = "Account/Register" });