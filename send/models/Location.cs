using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace MarsRoverChallenge.Send.Models;

public struct Location : IEquatable<Location>
{
    private static readonly Regex _locationPattern = new Regex(@"^\s*(?'x'[-]?\d+)[ ]+(?'y'[-]?\d+)[ ]+(?'o'[NnSsWwEe])\s*$");

    public Location(int x, int y, Direction orientation) : this (new Coordinate(x, y), orientation)
    { }

    public Location(Coordinate position, Direction orientation)
    {
        Position = position;
        Orientation = orientation;
    }

    public Coordinate Position { get; set; }
    public Direction Orientation { get; set; }

    public override string ToString()
    {
        return $"{Position} {Orientation}";
    }

    public static bool TryParse(string value, out Location parsed)
    {
        parsed = new Location();
        var match = _locationPattern.Match(value);
        if (match.Success == false)
        {
            return false;
        }

        var x = int.Parse(match.Groups["x"].Value);
        var y = int.Parse(match.Groups["y"].Value);

        parsed.Position = new Coordinate(x, y);
        parsed.Orientation = Enum.Parse<Direction>(match.Groups["o"].Value, true);
        return true;
    }

    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Location ? Equals((Location)obj) : false;
    public bool Equals(Location other) => Position == other.Position && Orientation == other.Orientation;
    public static bool operator ==(Location value1, Location value2) => value1.Equals(value2);
    public static bool operator !=(Location value1, Location value2) => value1.Equals(value2) == false;
    public override int GetHashCode() => HashCode.Combine(Position, Orientation);
}

