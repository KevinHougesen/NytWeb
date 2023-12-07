using NytWeb.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using NytWeb.Services;
using Blazored.Modal;
using MudBlazor.Services;
using Neo4j.Driver;



internal class Program
{
    static readonly HttpClient client = new HttpClient();

    private static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        var uri = NytWeb.Config.UnpackContextConfig();
        client.BaseAddress = new Uri(uri);

        builder.Services.AddSingleton(client);
        builder.Services.AddSingleton(GraphDatabase.Driver(
            builder.Configuration.GetConnectionString("NEO4J_URI"),
            AuthTokens.Basic(
                builder.Configuration.GetConnectionString("NEO4J_USER"),
                builder.Configuration.GetConnectionString("NEO4J_PASSWORD")
            )
        ));



        // Add services to the container.
        builder.Services.AddAuthenticationCore();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddScoped<ProtectedSessionStorage>();
        builder.Services.AddBlazoredModal();
        builder.Services.AddMudServices();
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IPostService, PostService>();




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

    }



}