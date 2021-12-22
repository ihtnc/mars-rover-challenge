using MarsRoverChallenge.Send.Models;

namespace MarsRoverChallenge.Send;

public class ProcessorStartEventArgs : EventArgs
{
    public Input RunInput { get; set; }

    public ProcessorStartEventArgs(Input input) => RunInput = input;
}