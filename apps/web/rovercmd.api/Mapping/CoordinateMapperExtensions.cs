using MarsRoverChallenge.Apps.Web.Models;

namespace MarsRoverChallenge.Apps.Web.Mapping;

public static class CoordinateMapperExtensions
{
    public static Send.Models.Coordinate ToSendCoordinate(this Coordinate value) => new Send.Models.Coordinate
    {
        X = value.X,
        Y = value.Y
    };

    public static Coordinate ToCoordinate(this Send.Models.Coordinate value) => new Coordinate
    {
        X = value.X,
        Y = value.Y
    };
}