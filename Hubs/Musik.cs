using Microsoft.AspNetCore.SignalR;
using NytWeb.Services;
using Azure.Messaging.WebPubSub.Clients;
using Microsoft.Extensions.Azure;
using Microsoft.Azure.WebPubSub.AspNetCore;
using Microsoft.Azure.WebPubSub.Common;

namespace Hubs;

public class Musik : WebPubSubHub
{
    private readonly WebPubSubServiceClient<Musik> _serviceClient;

    public Musik(WebPubSubServiceClient<Musik> serviceClient)
    {
        _serviceClient = serviceClient;

    }

    public override async Task OnConnectedAsync(ConnectedEventRequest request)
    {
        await _serviceClient.SendToAllAsync($"[SYSTEM] {request.ConnectionContext.UserId} joined.");
        Console.WriteLine("Joined simplechat");
    }

    public override async ValueTask<UserEventResponse> OnMessageReceivedAsync(UserEventRequest request, CancellationToken cancellationToken)
    {
        await _serviceClient.SendToAllAsync($"[{request.ConnectionContext.UserId}] {request.Data}");
        Console.WriteLine("Joined simplechat");

        return request.CreateResponse($"[SYSTEM] ack.");
    }
}