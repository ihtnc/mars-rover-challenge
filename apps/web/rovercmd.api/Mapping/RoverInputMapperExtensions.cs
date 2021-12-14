using MarsRoverChallenge.Apps.Web.Models;

namespace MarsRoverChallenge.Apps.Web.Mapping;

public static class RoverInputMapperExtensions
{
    public static Send.Models.RoverCommandInput ToSendRoverCommandInput(this RoverInput value) => new Send.Models.RoverCommandInput
    {
        StartingLocation = value.InitialLocation.ToSendLocation(),
        Commands = value.Commands.ToSendCommands()
    };

    public static IEnumerable<Send.Models.RoverCommandInput> ToSendRoverCommandInputs(this IEnumerable<RoverInput> values)
    {
        var mapped = values?.Select(v => v.ToSendRoverCommandInput()) ?? new Send.Models.RoverCommandInput[0];
        return mapped;
    }
}