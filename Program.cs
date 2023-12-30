using NytWeb.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using NytWeb.Services;
using Blazored.Modal;
using MudBlazor.Services;
using Microsoft.AspNetCore.ResponseCompression;
using BlazorSignalRApp.Hubs;
using Azure.Storage.Blobs;

internal class Program
{
    static readonly HttpClient client = new HttpClient();

    private static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        var BlobKey = NytWeb.Config.UnpackBlobConfig();

        builder.Services.AddAuthenticationCore();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddBlazoredModal();
        builder.Services.AddMudServices();
        builder.Services.AddSingleton(client);
        builder.Services.AddScoped<ProtectedSessionStorage>();
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IBlobService, BlobService>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddSingleton(x => new BlobServiceClient(BlobKey));
        builder.Services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { "application/octet-stream" });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseResponseCompression();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapHub<ChatHub>("/chathub");
        app.MapHub<PostHub>("/posthub");
        app.MapFallbackToPage("/_Host");

        app.Run();

    }

}