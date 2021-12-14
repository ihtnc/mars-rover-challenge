using MarsRoverChallenge.Apps.Web.Models;

namespace MarsRoverChallenge.Apps.Web.Mapping;

public static class LocationMapperExtensions
{
    public static Send.Models.Location ToSendLocation(this Location value) => new Send.Models.Location
    {
        Position = value.Position.ToSendCoordinate(),
        Orientation = value.Orientation.ToSendDirection()
    };

    public static Location ToLocation(this Send.Models.Location value) => new Location
    {
        Position = value.Position.ToCoordinate(),
        Orientation = value.Orientation.ToDirection()
    };

    public static IEnumerable<Location> ToLocations(this IEnumerable<Send.Models.Location> value)
    {
        var mapped = value?.Select(v => v.ToLocation()) ?? new Location[0];
        return mapped;
    }
}