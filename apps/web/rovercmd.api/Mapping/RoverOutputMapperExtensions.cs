using MarsRoverChallenge.Apps.Web.Models;

namespace MarsRoverChallenge.Apps.Web.Mapping;

public static class RoverOutputMapperExtensions
{
    public static RoverOutput ToRoverOutput(this Send.Models.RoverCommandOutput value) => new RoverOutput
    {
        Id = value.Id,
        Location = value.Location.ToLocation()
    };

    public static IEnumerable<RoverOutput> ToRoverOutputs(this IEnumerable<Send.Models.RoverCommandOutput> values)
    {
        var mapped = values?.Select(v => v.ToRoverOutput()) ?? new RoverOutput[0];
        return mapped;
    }
}