namespace MarsRoverChallenge.Send.Models;

public class RoverCommandInput
{
    public Location StartingLocation { get; set; }
    public IEnumerable<Command> Commands { get; set; } = new Command[0];
}
