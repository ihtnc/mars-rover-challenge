using Microsoft.AspNetCore.SignalR;

namespace MarsRoverChallenge.Apps.Web.Hubs;

public class RoverHub : Hub<IRoverClient>
{
    public async Task SendRoverUpdate(string update)
    {
        await Clients.All.ReceiveUpdate(update);
    }
}