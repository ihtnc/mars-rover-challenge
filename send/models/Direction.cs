using System.ComponentModel;

namespace MarsRoverChallenge.Send.Models;

public enum Direction
{
    [Description("Represents a north-facing direction")]
    N,

    [Description("Represents an east-facing direction")]
    E,

    [Description("Represents a south-facing direction")]
    S,

    [Description("Represents a west-facing direction")]
    W
}