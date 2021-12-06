using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace MarsRoverChallenge.Send.Models;

public struct Coordinate : IEquatable<Coordinate>
{
    private static readonly Regex _coordinatePattern = new Regex(@"^\s*(?'x'[-]?\d+)[ ]+(?'y'[-]?\d+)\s*$");

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public override string ToString()
    {
        return $"{X} {Y}";
    }

    public static bool TryParse(string value, out Coordinate parsed)
    {
        parsed = new Coordinate();
        var match = _coordinatePattern.Match(value);
        if (match.Success == false)
        {
            return false;
        }

        parsed.X = int.Parse(match.Groups["x"].Value);
        parsed.Y = int.Parse(match.Groups["y"].Value);
        return true;
    }

    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Coordinate ? Equals((Coordinate)obj) : false;
    public bool Equals(Coordinate other) => X == other.X && Y == other.Y;
    public static bool operator ==(Coordinate value1, Coordinate value2) => value1.Equals(value2);
    public static bool operator !=(Coordinate value1, Coordinate value2) => value1.Equals(value2) == false;
    public override int GetHashCode() => HashCode.Combine(X, Y);
}