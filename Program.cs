using NytWeb.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using NytWeb.Services;
using Blazored.Modal;
using NytWeb.Configuration;
using MudBlazor.Services;
using Microsoft.AspNetCore.ResponseCompression;
using BlazorSignalRApp.Hubs;
using Microsoft.Azure.WebPubSub.AspNetCore;
using Azure.Storage.Blobs;
using Azure.Messaging.WebPubSub;
using Microsoft.Azure.WebPubSub.AspNetCore;
using Microsoft.Extensions.Azure;
using Microsoft.Azure.WebPubSub.Common;
using Hubs;
using Syncfusion.Blazor;

HttpClient client = new HttpClient();


var builder = WebApplication.CreateBuilder(args);
var BlobKey = builder.Configuration.GetValue<string>("AzureBlobStorage");
var WebPubSubKey = builder.Configuration.GetValue<string>("AzureWebPubSub");

builder.Services.AddWebPubSub(
    o => o.ServiceEndpoint = new WebPubSubServiceEndpoint(WebPubSubKey))
    .AddWebPubSubServiceClient<Sample_ChatApp>();

builder.Services.AddAuthenticationCore();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
builder.Services.AddBlazoredModal();
builder.Services.AddMudServices();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddSingleton(client);
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddTransient<IConfig, Config>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBlobService, BlobService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddSingleton(x => new BlobServiceClient(BlobKey));
builder.Services.AddRazorComponents().AddInteractiveServerComponents()
    .AddHubOptions(options =>
{
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
    options.HandshakeTimeout = TimeSpan.FromSeconds(30);
});
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/negotiate", async (WebPubSubServiceClient<Sample_ChatApp> serviceClient, HttpContext context) =>
    {
        var id = context.Request.Query["id"];
        var firstGroup = context.Request.Query["firstGroup"];
        var secondGroup = context.Request.Query["secondGroup"];
        var thirdGroup = context.Request.Query["thirdGroup"];
        if (id.Count != 1)
        {

            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("missing user id");
            return;
        }
        Console.WriteLine("User: " + id);
        await context.Response.WriteAsync(serviceClient.GetClientAccessUri(userId: id,
        roles: [
            $"webpubsub.sendToGroup.{firstGroup}",
            $"webpubsub.joinLeaveGroup.{firstGroup}",

            $"webpubsub.sendToGroup.{secondGroup}",
            $"webpubsub.joinLeaveGroup.{secondGroup}",

            $"webpubsub.sendToGroup.{thirdGroup}",
            $"webpubsub.joinLeaveGroup.{thirdGroup}"
            ]).AbsoluteUri);
    });

    endpoints.MapWebPubSubHub<Sample_ChatApp>("/eventhandler/{*path}");
});


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();