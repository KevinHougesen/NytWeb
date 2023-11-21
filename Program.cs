using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NytWeb.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;
using NytWeb.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using NytWeb.Data;
using NytWeb.Algorithms;
using NytWeb.Services;
using Blazored.Modal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<ContentFilter>();
builder.Services.AddScoped<ContentRanker>();
builder.Services.AddBlazoredModal();
// builder.Services.AddScoped<DbContext, ApiContext>();
builder.Services.AddDbContext<ApiContext>
    (opt => opt.UseCosmos(
        builder.Configuration.GetConnectionString("CosmosUriString"),
        builder.Configuration.GetConnectionString("CosmosKeyString"),
        builder.Configuration.GetConnectionString("CosmosDbString")));
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
