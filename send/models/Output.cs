namespace MarsRoverChallenge.Send.Models;

public class Output
{
    public IEnumerable<RoverCommandOutput> RoverOutputs { get; set; } = new RoverCommandOutput[0];
}
