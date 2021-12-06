using System.ComponentModel;

namespace MarsRoverChallenge.Send.Models;

public enum Command
{
    [Description("Represents the command for turning left")]
    L,

    [Description("Represents the command for turning right")]
    R,

    [Description("Represents the command for moving forward")]
    M
}