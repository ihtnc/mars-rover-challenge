namespace MarsRoverChallenge.Send.Models;

public class Input
{
    public LandscapeData LandscapeDetails { get; set; } = new LandscapeData();
    public IEnumerable<RoverCommandInput> RoverCommands { get; set; } = new RoverCommandInput[0];
}