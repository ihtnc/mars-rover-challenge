using MarsRoverChallenge.Send.Models;

namespace MarsRoverChallenge.Send.Engine;

public class CommandExecutionEventArgs : EventArgs
{
    public bool Success { get; set; }
    public Command ExecutedCommand { get; set; }
    public Location Previous { get; set; }
    public Location Current { get; set; }
}