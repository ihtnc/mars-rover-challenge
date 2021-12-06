using MarsRoverChallenge.Send.Engine;

namespace MarsRoverChallenge.Send.Providers;

public interface IRoverProvider
{
    IRover CreateRover();
}

public class RoverProvider : IRoverProvider
{
    public IRover CreateRover() => new Rover();
}