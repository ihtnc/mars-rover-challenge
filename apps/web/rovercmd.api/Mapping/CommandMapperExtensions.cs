using MarsRoverChallenge.Apps.Web.Models;

namespace MarsRoverChallenge.Apps.Web.Mapping;

public static class CommandMapperExtensions
{
    public static Send.Models.Command ToSendCommand(this Command value)
    {
        switch (value)
        {
            case Command.L: return Send.Models.Command.L;
            case Command.R: return Send.Models.Command.R;
            case Command.M: return Send.Models.Command.M;
            default:
                throw new NotImplementedException();
        };
    }

    public static IEnumerable<Send.Models.Command> ToSendCommands(this IEnumerable<Command> values)
    {
        var mapped = values?.Select(v => v.ToSendCommand()) ?? new Send.Models.Command[0];
        return mapped;
    }
}