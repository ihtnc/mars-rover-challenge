using System.Text.Json.Serialization;

namespace MarsRoverChallenge.Apps.Web.Models;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum Direction
{
    N,
    E,
    S,
    W
}