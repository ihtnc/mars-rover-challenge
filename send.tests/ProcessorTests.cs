using System;
using System.Collections.Generic;
using System.Linq;
using MarsRoverChallenge.Send.Engine;
using MarsRoverChallenge.Send.Models;
using MarsRoverChallenge.Send.Providers;
using Xunit;
using FluentAssertions;
using NSubstitute;

namespace MarsRoverChallenge.Send.Tests;

public class ProcessorTests
{
    private readonly IRoverProvider _roverProvider;
    private readonly ILandscapeProvider _landscapeProvider;
    private readonly IRover _rover;
    private readonly ILandscape _landscape;
    private readonly IProcessor _processor;

    public ProcessorTests()
    {
        _roverProvider = Substitute.For<IRoverProvider>();
        _landscapeProvider = Substitute.For<ILandscapeProvider>();

        _rover = Substitute.For<IRover>();
        _roverProvider.CreateRover().Returns(_rover);

        _landscape = Substitute.For<ILandscape>();
        _landscape.AddMarker(Arg.Any<Coordinate>()).Returns(true);
        _landscapeProvider.CreateLandscape(Arg.Any<Coordinate>(), Arg.Any<Coordinate>()).Returns(_landscape);

        _processor = new Processor(_roverProvider, _landscapeProvider);
    }

    [Fact]
    public void Run_Should_Create_Landscape()
    {
        // ARRANGE
        var bottomLeftCorner = new Coordinate(1, 1);
        var upperRightCorner = new Coordinate(2, 2);
        var input = new Input
        {
            LandscapeDetails = new LandscapeData
            {
                BottomLeftCorner = bottomLeftCorner,
                UpperRightCorner = upperRightCorner
            }
        };

        // ACT
        _processor.Run(input);

        // ASSERT
        _landscapeProvider.Received(1).CreateLandscape(bottomLeftCorner, upperRightCorner);
    }

    [Fact]
    public void Run_Should_Add_Landscape_Markers_For_Each_Rover_Command()
    {
        // ARRANGE
        var input = CreateInput(3);

        // ACT
        _processor.Run(input);

        // ASSERT
        foreach(var command in input.RoverCommands)
        {
            _landscape.Received(1).AddMarker(command.StartingLocation.Position);
        }
    }

    [Fact]
    public void Run_Should_Throw_Exception_If_Adding_Landscape_Marker_Fails()
    {
        // ARRANGE
        _landscape.AddMarker(Arg.Any<Coordinate>()).Returns(false);
        var input = CreateInput();

        // ACT
        var action = () => _processor.Run(input);

        // ASSERT
        var command = input.RoverCommands.First();
        var message = $"Unable to set rover on location ({command.StartingLocation}): Location already occupied.";
        action.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Run_Should_Create_Rover_For_Each_Rover_Command()
    {
        // ARRANGE
        var commandCount = 4;
        var input = CreateInput(commandCount);

        // ACT
        _processor.Run(input);

        // ASSERT
        _roverProvider.Received(commandCount).CreateRover();
    }

    [Fact]
    public void Run_Should_Initialise_Rover_For_Each_Rover_Command()
    {
        // ARRANGE
        var commandCount = 2;
        var input = CreateInput(commandCount);

        // ACT
        _processor.Run(input);

        // ASSERT
        foreach(var command in input.RoverCommands)
        {
            _rover.Received(1).Initialise(command.StartingLocation, Arg.Any<Func<Location, bool>>());
        }
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void Run_Should_Pass_Correct_Probe_Function_On_Rover_Initialisation(bool isOccupied, bool expected)
    {
        // ARRANGE
        var currentPosition = new Coordinate(123, 456);

        Func<Location, bool> probeFunction = location => throw new NotImplementedException();
        _rover.Initialise(Arg.Any<Location>(), Arg.Do<Func<Location, bool>>(arg => probeFunction = arg));
        _landscape.IsOccupied(currentPosition).Returns(isOccupied);

        var current = new Location(currentPosition, Direction.E);

        // ACT
        var input = CreateInput();
        _processor.Run(input);
        var actual = probeFunction(current);

        // ASSERT
        _landscape.Received(1).IsOccupied(current.Position);
        actual.Should().Be(expected);
    }

    [Fact]
    public void Run_Should_Subscribe_To_Rover_Event_For_Each_Rover_Command()
    {
        // ARRANGE
        var commandCount = 3;
        var input = CreateInput(commandCount);

        // ACT
        _processor.Run(input);

        // ASSERT
        foreach(var command in input.RoverCommands)
        {
            _rover.Received(commandCount).OnCommandExecution += Arg.Any<CommandExecutionHandler>();
        }
    }

    [Theory]
    [InlineData(true, Command.M, 1)]
    [InlineData(true, Command.L, 0)]
    [InlineData(false, Command.M, 0)]
    [InlineData(false, Command.R, 0)]
    public void Run_Should_Subscribe_Correct_Handler_For_Rover_Event(bool eventSuccess, Command eventCommand, int expectedCall)
    {
        // ARRANGE
        _landscape.MoveMarker(Arg.Any<Coordinate>(), Arg.Any<Coordinate>()).Returns(true);

        var previous = new Coordinate(123, 456);
        var current = new Coordinate(135, 246);
        var args = new CommandExecutionEventArgs
        {
            Success = eventSuccess,
            ExecutedCommand = eventCommand,
            Previous = new Location(previous, Direction.E),
            Current = new Location(current, Direction.S)
        };

        // ACT
        var input = CreateInput();
        _processor.Run(input);
        _rover.OnCommandExecution += Raise.Event<CommandExecutionHandler>(_rover, args);

        // ASSERT
        _landscape.Received(expectedCall).MoveMarker(previous, current);
    }

    [Fact]
    public void Run_Should_Throw_Exception_When_Moving_Landscape_Markers_Within_Rover_Event()
    {
        // ARRANGE
        _landscape.MoveMarker(Arg.Any<Coordinate>(), Arg.Any<Coordinate>()).Returns(false);

        var previous = new Location(135, 246, Direction.W);
        var current = new Location(123, 456, Direction.E);

        var args = new CommandExecutionEventArgs
        {
            Success = true,
            ExecutedCommand = Command.M,
            Previous = previous,
            Current = current
        };

        // ACT
        var input = CreateInput();
        _processor.Run(input);
        var action = () => _rover.OnCommandExecution += Raise.Event<CommandExecutionHandler>(_rover, args);

        // ASSERT
        var message = $"Unexpected error moving rover location from ({previous}) to ({current}).";
        action.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Run_Should_Send_Commands_To_Each_Rover()
    {
        // ARRANGE
        var commandCount = 3;
        var input = CreateInput(commandCount);

        var commandMap = new Dictionary<int, Command[]>();
        commandMap.Add(0, new [] {Command.L, Command.R});
        commandMap.Add(1, new [] {Command.M, Command.M});
        commandMap.Add(2, new [] {Command.R, Command.R, Command.M, Command.L});

        var commands = input.RoverCommands.ToArray();
        for(var i = 0; i < commandCount; i++)
        {
            commands[i].Commands = commandMap[i];
        }

        // ACT
        _processor.Run(input);

        // ASSERT
        foreach(var command in input.RoverCommands)
        {
            _rover.Received(1).ExecuteCommands(command.Commands.ToArray());
        }
    }

    [Fact]
    public void Run_Return_Current_Output_Of_Each_Rover()
    {
        // ARRANGE
        var commandCount = 3;
        var input = CreateInput(commandCount);

        var output1 = new RoverCommandOutput
        {
            Id = "111",
            Location = new Location(111, 222, Direction.W)
        };

        var output2 = new RoverCommandOutput
        {
            Id = "333",
            Location = new Location(333, 444, Direction.E)
        };

        var output3 = new RoverCommandOutput
        {
            Id = "555",
            Location = new Location(555, 666, Direction.N)
        };

        var outputs = new [] { output1, output2, output3 };

        var rover1 = Substitute.For<IRover>();
        rover1.Id.Returns(output1.Id);
        rover1.CurrentLocation.Returns(output1.Location);

        var rover2 = Substitute.For<IRover>();
        rover2.Id.Returns(output2.Id);
        rover2.CurrentLocation.Returns(output2.Location);

        var rover3 = Substitute.For<IRover>();
        rover3.Id.Returns(output3.Id);
        rover3.CurrentLocation.Returns(output3.Location);

        _roverProvider.CreateRover().Returns(rover1, rover2, rover3);

        // ACT
        var output = _processor.Run(input);

        // ASSERT
        output.RoverOutputs.Should().HaveCount(commandCount);
        output.RoverOutputs.Should().BeEquivalentTo(outputs);
    }

    private Input CreateInput(int commandCount = 1)
    {
        var commands = new List<RoverCommandInput>();
        for (var i = 0; i < commandCount; i++)
        {
            var command = new RoverCommandInput
            {
                StartingLocation = new Location(i, i, Direction.S)
            };
            commands.Add(command);
        }

        var input = new Input
        {
            LandscapeDetails = new LandscapeData(),
            RoverCommands = commands
        };

        return input;
    }
}