using MarsRoverChallenge.Send.Models;

namespace MarsRoverChallenge.Send;

public class ProcessorEndEventArgs : EventArgs
{
    public Output RunOutput { get; set; }

    public ProcessorEndEventArgs(Output output) => RunOutput = output;
}