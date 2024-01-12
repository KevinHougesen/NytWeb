using Microsoft.AspNetCore.SignalR;
using Azure.Messaging.WebPubSub;
using Websocket.Client;

namespace BlazorSignalRApp.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}