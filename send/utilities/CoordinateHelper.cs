using MarsRoverChallenge.Send.Models;

namespace MarsRoverChallenge.Send.Utilities;

public static class CoordinateHelper
{
    /// <summary>
    /// Returns the next clockwise direction relative to the current one.
    /// </summary>
    public static T[,] ToMap<T>(Coordinate bottomLeftCorner, Coordinate upperRightCorner)
    {
        var xLength = upperRightCorner.X - bottomLeftCorner.X;
        var yLength = upperRightCorner.Y - bottomLeftCorner.Y;

        return new T[xLength + 1,yLength + 1];
    }

    public static bool IsWithinBounds(Coordinate bottomLeftCorner, Coordinate upperRightCorner, Coordinate location)
    {
        if (location.X < bottomLeftCorner.X) { return false; }
        if (location.Y < bottomLeftCorner.Y) { return false; }
        if (location.X > upperRightCorner.X) { return false; }
        if (location.Y > upperRightCorner.Y) { return false; }

        return true;
    }
}