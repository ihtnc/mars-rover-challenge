using MarsRoverChallenge.Send.Models;

namespace MarsRoverChallenge.Apps.Console;

public static class InputHelper
{
    const int UPPER_RIGHT_CORNER_END_INDEX = 0;

    public static Input ParseInput(string value)
    {
        var lines = value.Split(new [] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length < 3 || lines.Length % 2 == 0)
        {
            throw new FormatException("Unrecognised file content.");
        }

        var upperRightCorner = lines[UPPER_RIGHT_CORNER_END_INDEX];
        var upperRightCornerValue = ParseCoordinate(upperRightCorner);

        var roverCommandsValue = new List<RoverCommandInput>();

        for (int i = UPPER_RIGHT_CORNER_END_INDEX + 1; i < lines.Length; i+=2)
        {
            var initialPosition = lines[i];
            var initialPositionValue = ParseLocation(initialPosition);

            var commands = lines[i+1];
            var commandsValue = ParseCommands(commands);

            var commandInput = new RoverCommandInput();
            commandInput.StartingLocation = initialPositionValue;
            commandInput.Commands = commandsValue;

            roverCommandsValue.Add(commandInput);
        }

        return new Input
        {
            LandscapeDetails = new LandscapeData
            {
                BottomLeftCorner = new Coordinate(0, 0),
                UpperRightCorner = upperRightCornerValue
            },
            RoverCommands = roverCommandsValue
        };
    }

    private static Coordinate ParseCoordinate(string value)
    {
        if (Coordinate.TryParse(value, out var coordinate) == false)
        {
            throw new FormatException("Unrecognised format found while reading the coordinate data.");
        }

        return coordinate;
    }

    private static Location ParseLocation(string value)
    {
        if (Location.TryParse(value, out var locationValue) == false)
        {
            throw new FormatException("Unrecognised format found while reading the location data.");
        }

        return locationValue;
    }

    private static IEnumerable<Command> ParseCommands(string value)
    {
        var commands = new List<Command>();

        foreach(var c in value)
        {
            if (Enum.TryParse<Command>($"{c}", true, out var command) == false)
            {
                throw new FormatException("Unrecognised format found while reading the command data.");
            }

            commands.Add(command);
        }

        return commands;
    }
}