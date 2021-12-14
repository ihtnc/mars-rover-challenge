using MarsRoverChallenge.Apps.Web.Models;

namespace MarsRoverChallenge.Apps.Web.Mapping;

public static class DirectionMapperExtensions
{
    public static Send.Models.Direction ToSendDirection(this Direction value)
    {
        switch (value)
        {
            case Direction.N: return Send.Models.Direction.N;
            case Direction.E: return Send.Models.Direction.E;
            case Direction.S: return Send.Models.Direction.S;
            case Direction.W: return Send.Models.Direction.W;
            default:
                throw new NotImplementedException();
        };
    }

    public static Direction ToDirection(this Send.Models.Direction value)
    {
        switch (value)
        {
            case Send.Models.Direction.N: return Direction.N;
            case Send.Models.Direction.E: return Direction.E;
            case Send.Models.Direction.S: return Direction.S;
            case Send.Models.Direction.W: return Direction.W;
            default:
                throw new NotImplementedException();
        };
    }
}