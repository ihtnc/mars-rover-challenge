using MarsRoverChallenge.Apps.Web.Models;

namespace MarsRoverChallenge.Apps.Web.Mapping;

public static class SendRequestMapperExtensions
{
    public static Send.Models.Input ToSendInput(this SendRequest value)
    {
        var mapped = new Send.Models.Input
        {
            LandscapeDetails = new Send.Models.LandscapeData
            {
                BottomLeftCorner = new MarsRoverChallenge.Send.Models.Coordinate(0, 0),
                UpperRightCorner = value.LandscapeBoundary.ToSendCoordinate()
            },
            RoverCommands = value.RoverCommands.ToSendRoverCommandInputs()
        };

        return mapped;
    }
}