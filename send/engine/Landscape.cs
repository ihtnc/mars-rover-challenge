using MarsRoverChallenge.Send.Utilities;
using MarsRoverChallenge.Send.Models;

namespace MarsRoverChallenge.Send.Engine;

public interface ILandscape
{
    Coordinate BottomLeftCorner { get; }
    Coordinate UpperRightCorner { get; }

    void Initialise(Coordinate bottomLeftCorner, Coordinate upperRightCorner);
    bool AddMarker(Coordinate location);
    bool RemoveMarker(Coordinate location);
    bool MoveMarker(Coordinate currentLocation, Coordinate newLocation);
    bool? IsOccupied(Coordinate location);
    bool IsWithinBounds(Coordinate location);
}

public class Landscape : ILandscape
{
    public Coordinate BottomLeftCorner { get; private set; }
    public Coordinate UpperRightCorner { get; private set; }

    private bool[,] _map = new bool[0,0];

    public Landscape(Coordinate bottomLeftCorner, Coordinate upperRightCorner)
    {
       Initialise(bottomLeftCorner, upperRightCorner);
    }

    public void Initialise(Coordinate bottomLeftCorner, Coordinate upperRightCorner)
    {
        BottomLeftCorner = bottomLeftCorner;
        UpperRightCorner = upperRightCorner;
        _map = CoordinateHelper.ToMap<bool>(bottomLeftCorner, upperRightCorner);
    }

    public bool AddMarker(Coordinate location)
    {
        if (IsWithinBounds(location) == false)
        {
            return false;
        }

        _map[location.X, location.Y] = true;
        return true;
    }

    public bool RemoveMarker(Coordinate location)
    {
        if (IsWithinBounds(location) == false)
        {
            return false;
        }

        _map[location.X, location.Y] = false;
        return true;
    }

    public bool MoveMarker(Coordinate currentLocation, Coordinate newLocation)
    {
        if (RemoveMarker(currentLocation) == false) { return false; }

        if (AddMarker(newLocation) == false)
        {
            // revert the marker on the current location since the new one failed
            AddMarker(currentLocation);
            return false;
        }

        return true;
    }

    public bool? IsOccupied(Coordinate location)
    {
        if (IsWithinBounds(location) == false)
        {
            return null;
        }

        return _map[location.X, location.Y];
    }

    public bool IsWithinBounds(Coordinate location) => CoordinateHelper.IsWithinBounds(BottomLeftCorner, UpperRightCorner, location);
}