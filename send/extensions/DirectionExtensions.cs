using MarsRoverChallenge.Send.Models;

namespace MarsRoverChallenge.Send.Extensions;

public static class DirectionExtensions
{
    /// <summary>
    /// Returns the next clockwise direction relative to the current one.
    /// </summary>
    public static Direction Turn(this Direction current, bool reverse = false)
    {
        switch (current)
        {
            case Direction.N:
                return reverse ? Direction.W : Direction.E;
            case Direction.E:
                return reverse ? Direction.N : Direction.S;
            case Direction.S:
                return reverse ? Direction.E : Direction.W;
            case Direction.W:
                return reverse ? Direction.S : Direction.N;
            default:
                return current;
        }
    }
}