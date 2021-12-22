using MarsRoverChallenge.Send.Models;
using MarsRoverChallenge.Send.Providers;

namespace MarsRoverChallenge.Send;

public interface IProcessor
{
    Output Run(Input input);

    event EventHandler<ProcessorStartEventArgs>? OnProcessorStart;
    event EventHandler<ProcessorEndEventArgs>? OnProcessorEnd;
}

public class Processor : IProcessor
{
    private readonly IRoverProvider _roverProvider;
    private readonly ILandscapeProvider _landscapeProvider;

    public Processor() : this(new RoverProvider(), new LandscapeProvider()) {}
    public Processor(IRoverProvider roverProvider, ILandscapeProvider landscapeProvider)
    {
        _roverProvider = roverProvider;
        _landscapeProvider = landscapeProvider;
    }

    public Output Run(Input input)
    {
        OnProcessorStart?.Invoke(this, new ProcessorStartEventArgs(input));

        var landscape = _landscapeProvider.CreateLandscape(input.LandscapeDetails.BottomLeftCorner, input.LandscapeDetails.UpperRightCorner);
        var roverOutputs = new List<RoverCommandOutput>();

        foreach(var cmd in input.RoverCommands)
        {
            if (landscape.AddMarker(cmd.StartingLocation.Position) == false)
            {
                throw new InvalidOperationException($"Unable to set rover on location ({cmd.StartingLocation}): Location already occupied.");
            }

            var rover = _roverProvider.CreateRover();
            rover.Initialise(cmd.StartingLocation, current => landscape.IsOccupied(current.Position) == false);
            rover.OnCommandExecute += (sender, args) =>
            {
                if (args.Success == false || args.ExecutedCommand != Command.M) { return; }

                if (landscape.MoveMarker(args.Previous.Position, args.Current.Position) == false)
                {
                    throw new InvalidOperationException($"Unexpected error moving rover location from ({args.Previous}) to ({args.Current}).");
                }
            };

            rover.ExecuteCommands(cmd.Commands.ToArray());

            var roverOutput = new RoverCommandOutput
            {
                Id = rover.Id,
                Location = rover.CurrentLocation
            };
            roverOutputs.Add(roverOutput);
        }

        var output = new Output
        {
            RoverOutputs = roverOutputs
        };

        OnProcessorEnd?.Invoke(this, new ProcessorEndEventArgs(output));

        return output;
    }

    public event EventHandler<ProcessorStartEventArgs>? OnProcessorStart;
    public event EventHandler<ProcessorEndEventArgs>? OnProcessorEnd;
}