namespace MarsRoverChallenge.Apps.Web.Hubs;

public interface IRoverClient
{
    Task ReceiveUpdate(string update);
}