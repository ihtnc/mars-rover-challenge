namespace MarsRoverChallenge.Apps.Web.Models;

public class RoverInput
{
    public Location InitialLocation { get; set; }
    public IEnumerable<Command> Commands { get; set; } = new Command[0];
}