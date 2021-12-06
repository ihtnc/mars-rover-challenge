using MarsRoverChallenge.Send.Models;

namespace MarsRoverChallenge.Send.Extensions;

public static class LocationExtensions
{
    /// <summary>
    /// Moves the location 1 step forward relative to the current orientation.
    /// </summary>
    public static Location Forward(this Location current, bool reverse = false)
    {
        var xOffset = 0;
        var yOffset = 0;
        switch (current.Orientation)
        {
            case Direction.N:
                yOffset = (reverse ? -1 : 1);
                break;
            case Direction.E:
                xOffset = (reverse ? -1 : 1);
                break;
            case Direction.S:
                yOffset = (reverse ? 1 : -1);
                break;
            case Direction.W:
                xOffset = (reverse ? 1 : -1);
                break;
            default:
                break;
        }

        var newPosition = current.Position;
        newPosition.X += xOffset;
        newPosition.Y += yOffset;

        var newLocation = current;
        newLocation.Position = newPosition;
        return newLocation;
    }
}
