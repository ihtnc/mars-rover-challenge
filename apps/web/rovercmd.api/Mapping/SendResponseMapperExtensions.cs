using MarsRoverChallenge.Apps.Web.Models;

namespace MarsRoverChallenge.Apps.Web.Mapping;

public static class SendResponseMapperExtensions
{
    public static SendResponse ToSendResponse(this Send.Models.Output value)
    {
        var mapped = new SendResponse
        {
            RoverOutputs = value.RoverOutputs.ToRoverOutputs()
        };

        return mapped;
    }
}