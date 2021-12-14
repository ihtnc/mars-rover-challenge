using MarsRoverChallenge.Send;
using MarsRoverChallenge.Apps.Web.Models;
using MarsRoverChallenge.Apps.Web.Mapping;

namespace MarsRoverChallenge.Apps.Web.Services;

public interface IRoverCommandService
{
    SendResponse ProcessRequest(SendRequest request);
}

public class RoverCommandService : IRoverCommandService
{
    private readonly IProcessor _processor;

    public RoverCommandService(IProcessor processor)
    {
        _processor = processor;
    }

    public SendResponse ProcessRequest(SendRequest request)
    {
        var input = request.ToSendInput();
        var output = _processor.Run(input);

        return output.ToSendResponse();
    }
}