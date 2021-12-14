namespace MarsRoverChallenge.Apps.Web.Models;

public class SendResponse
{
    public IEnumerable<RoverOutput> RoverOutputs { get; set; } = new RoverOutput[0];
}
