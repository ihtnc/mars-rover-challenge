using MarsRoverChallenge.Send.Extensions;
using MarsRoverChallenge.Send.Models;
using MarsRoverChallenge.Send.Utilities;

namespace MarsRoverChallenge.Send.Engine;

public interface IRover
{
    string Id { get; }
    Location CurrentLocation { get; }

    void Initialise(Location currentLocation, Func<Location, bool> probeFunction);
    void TurnLeft();
    void TurnRight();
    void MoveForward();
    void ExecuteCommands(params Command[] commands);

    event EventHandler<CommandExecuteEventArgs>? OnCommandExecute;
}

public class Rover : IRover
{
    private Direction _currentDirection;
    private Coordinate _currentCoordinate;
    private Func<Location, bool> _probeFunction;

    public Rover()
    {
        var initialLocation = new Location(0, 0, Direction.N);

        _probeFunction = current => true;

        Initialise(initialLocation, _probeFunction);

        Id = StringHelper.GenerateShortId();
    }

    public string Id { get; }

    public Location CurrentLocation => new Location(_currentCoordinate, _currentDirection);

    public void Initialise(Location currentLocation, Func<Location, bool> probeFunction)
    {
        _currentDirection = currentLocation.Orientation;
        _currentCoordinate = currentLocation.Position;
        _probeFunction = probeFunction;
    }

    public void TurnLeft()
    {
        var previousState = CurrentLocation;
        _currentDirection = _currentDirection.Turn(true);

        OnCommandExecute?.Invoke(this, new CommandExecuteEventArgs
        {
            Success = true,
            ExecutedCommand = Command.L,
            Previous = previousState,
            Current = CurrentLocation
        });
    }

    public void TurnRight()
    {
        var previousState = CurrentLocation;
        _currentDirection = _currentDirection.Turn();

        OnCommandExecute?.Invoke(this, new CommandExecuteEventArgs
        {
            Success = true,
            ExecutedCommand = Command.R,
            Previous = previousState,
            Current = CurrentLocation
        });
    }

    public void MoveForward()
    {
        var previousState = CurrentLocation;
        var success = false;

        var newLocation = CurrentLocation.Forward();
        if (_probeFunction(newLocation))
        {
            _currentDirection = newLocation.Orientation;
            _currentCoordinate = newLocation.Position;

            success = true;
        }

        OnCommandExecute?.Invoke(this, new CommandExecuteEventArgs
        {
            Success = success,
            ExecutedCommand = Command.M,
            Previous = previousState,
            Current = CurrentLocation
        });
    }

    public void ExecuteCommands(params Command[] commands)
    {
        if (commands?.Any() != true) { return; }

        foreach(var cmd in commands)
        {
            switch (cmd)
            {
                case Command.L:
                    TurnLeft();
                    break;
                case Command.R:
                    TurnRight();
                    break;
                case Command.M:
                    MoveForward();
                    break;
                default:
                    continue;
            }
        }
    }

    public event EventHandler<CommandExecuteEventArgs>? OnCommandExecute;
}