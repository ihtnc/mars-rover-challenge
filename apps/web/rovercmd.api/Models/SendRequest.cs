namespace MarsRoverChallenge.Apps.Web.Models;

public class SendRequest
{
    public Coordinate LandscapeBoundary { get; set; }
    public IEnumerable<RoverInput> RoverCommands { get; set; } = new RoverInput[0];
}