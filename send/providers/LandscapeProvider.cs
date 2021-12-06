using MarsRoverChallenge.Send.Engine;
using MarsRoverChallenge.Send.Models;

namespace MarsRoverChallenge.Send.Providers;

public interface ILandscapeProvider
{
    ILandscape CreateLandscape(Coordinate bottomLeftCorner, Coordinate upperRightCorner);
}

public class LandscapeProvider : ILandscapeProvider
{
    public ILandscape CreateLandscape(Coordinate bottomLeftCorner, Coordinate upperRightCorner) => new Landscape(bottomLeftCorner, upperRightCorner);
}