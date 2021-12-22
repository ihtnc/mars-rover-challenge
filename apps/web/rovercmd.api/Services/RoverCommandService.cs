using MarsRoverChallenge.Send;
using MarsRoverChallenge.Apps.Web.Hubs;
using MarsRoverChallenge.Apps.Web.Models;
using MarsRoverChallenge.Apps.Web.Mapping;
using Microsoft.AspNetCore.SignalR;

namespace MarsRoverChallenge.Apps.Web.Services;

public interface IRoverCommandService
{
    SendResponse ProcessRequest(SendRequest request);
}

public class RoverCommandService : IRoverCommandService
{
    private readonly IProcessor _processor;
    private readonly IHubContext<RoverHub, IRoverClient> _hub;

    public RoverCommandService(IProcessor processor, IHubContext<RoverHub, IRoverClient> hub)
    {
        _processor = processor;
        _hub = hub;

        _processor.OnProcessorStart += OnProcessorStart;
    }

    public SendResponse ProcessRequest(SendRequest request)
    {
        var input = request.ToSendInput();
        var output = _processor.Run(input);
        return output.ToSendResponse();
    }

    public void OnProcessorStart(object? sender, ProcessorStartEventArgs args)
    {
        _hub.Clients.All.ReceiveUpdate(args.RunInput.LandscapeDetails.UpperRightCorner.ToString());
    }
}