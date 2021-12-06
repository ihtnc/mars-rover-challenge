using MarsRoverChallenge.Send.Engine;
using MarsRoverChallenge.Send.Extensions;
using MarsRoverChallenge.Send.Models;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Engine;

public class RoverTests
{
    private readonly IRover _rover;

    public RoverTests()
    {
        _rover = new Rover();
    }

    [Fact]
    public void Constructor_Should_Default_Current_Location()
    {
        // ARRANGE
        var location = new Location(0, 0, Direction.N);

        // ACT
        var rover = new Rover();

        // ASSERT
        rover.CurrentLocation.Should().Be(location);
    }

    [Fact]
    public void Constructor_Should_Generate_UniqueIds()
    {
        // ACT
        var rover1 = new Rover();
        var rover2 = new Rover();

        // ASSERT
        rover1.Id.Should().NotBe(rover2.Id);
    }

    [Fact]
    public void TurnLeft_Should_Return_Correctly()
    {
        // ARRANGE
        var expectedLocation = _rover.CurrentLocation;
        expectedLocation.Orientation = Direction.W;

        // ACT
        _rover.TurnLeft();

        // ASSERT
        _rover.CurrentLocation.Should().Be(expectedLocation);
    }

    [Fact]
    public void TurnLeft_Should_Raise_OnCommandExecution_Event()
    {
        // ARRANGE
        var eventCount = 0;
        _rover.OnCommandExecution += (sender, args) => { eventCount++; };

        var turnCount = 3;

        // ACT
        for(var i = 0; i < turnCount; i++)
        {
            _rover.TurnLeft();
        }

        // ASSERT
        eventCount.Should().Be(turnCount);
    }

    [Fact]
    public void TurnLeft_Should_Pass_Correct_EventArgs()
    {
        // ARRANGE
        var initialLocation = _rover.CurrentLocation;
        var newLocation = initialLocation;
        newLocation.Orientation = initialLocation.Orientation.Turn(true);

        var actualArgs = null as CommandExecutionEventArgs;

        _rover.OnCommandExecution += (sender, args) => { actualArgs = args; };

        // ACT
        _rover.TurnLeft();

        // ASSERT
        actualArgs.Should().NotBeNull();
        actualArgs?.Success.Should().BeTrue();
        actualArgs?.ExecutedCommand.Should().Be(Command.L);
        actualArgs?.Previous.Should().Be(initialLocation);
        actualArgs?.Current.Should().Be(newLocation);
    }

    [Fact]
    public void TurnRight_Should_Return_Correctly()
    {
        // ARRANGE
        var expectedLocation = _rover.CurrentLocation;
        expectedLocation.Orientation = Direction.E;

        // ACT
        _rover.TurnRight();

        // ASSERT
        _rover.CurrentLocation.Should().Be(expectedLocation);
    }

    [Fact]
    public void TurnRight_Should_Raise_OnCommandExecution_Event()
    {
        // ARRANGE
        var eventCount = 0;
        _rover.OnCommandExecution += (sender, args) => { eventCount++; };

        var turnCount = 3;

        // ACT
        for(var i = 0; i < turnCount; i++)
        {
            _rover.TurnRight();
        }

        // ASSERT
        eventCount.Should().Be(turnCount);
    }

    [Fact]
    public void TurnRight_Should_Pass_Correct_EventArgs()
    {
        // ARRANGE
        var initialLocation = _rover.CurrentLocation;
        var newLocation = initialLocation;
        newLocation.Orientation = initialLocation.Orientation.Turn();

        var actualArgs = null as CommandExecutionEventArgs;

        _rover.OnCommandExecution += (sender, args) => { actualArgs = args; };

        // ACT
        _rover.TurnRight();

        // ASSERT
        actualArgs.Should().NotBeNull();
        actualArgs?.Success.Should().BeTrue();
        actualArgs?.ExecutedCommand.Should().Be(Command.R);
        actualArgs?.Previous.Should().Be(initialLocation);
        actualArgs?.Current.Should().Be(newLocation);
    }

    [Fact]
    public void MoveForward_Should_Return_Correctly()
    {
        // ARRANGE
        var expectedLocation = new Location(0, 1, Direction.N);

        // ACT
        _rover.MoveForward();

        // ASSERT
        _rover.CurrentLocation.Should().Be(expectedLocation);
    }

    [Fact]
    public void MoveForward_Should_Return_Correctly_If_Probe_Function_Returns_False()
    {
        // ARRANGE
        var initialLocation = _rover.CurrentLocation;
        var expectedLocation = _rover.CurrentLocation;
        _rover.Initialise(initialLocation, location => false);

        // ACT
        _rover.MoveForward();

        // ASSERT
        _rover.CurrentLocation.Should().Be(expectedLocation);
    }

    [Fact]
    public void MoveForward_Should_Raise_OnCommandExecution_Event()
    {
        // ARRANGE
        var eventCount = 0;
        _rover.OnCommandExecution += (sender, args) => { eventCount++; };

        var forwardCount = 3;

        // ACT
        for(var i = 0; i < forwardCount; i++)
        {
            _rover.MoveForward();
        }

        // ASSERT
        eventCount.Should().Be(forwardCount);
    }

    [Fact]
    public void MoveForward_Should_Pass_Correct_EventArgs()
    {
        // ARRANGE
        var initialLocation = _rover.CurrentLocation;
        var newLocation = initialLocation.Forward();

        var actualArgs = null as CommandExecutionEventArgs;

        _rover.OnCommandExecution += (sender, args) => { actualArgs = args; };

        // ACT
        _rover.MoveForward();

        // ASSERT
        actualArgs.Should().NotBeNull();
        actualArgs?.Success.Should().BeTrue();
        actualArgs?.ExecutedCommand.Should().Be(Command.M);
        actualArgs?.Previous.Should().Be(initialLocation);
        actualArgs?.Current.Should().Be(newLocation);
    }

    [Fact]
    public void MoveForward_Should_Call_Probe_Function()
    {
        // ARRANGE
        var initialLocation = _rover.CurrentLocation;
        var callCount = 0;

        _rover.Initialise(initialLocation, location =>
        {
            callCount++;
            return true;
        });

        // ACT
        var forwardCount = 3;

        // ACT
        for(var i = 0; i < forwardCount; i++)
        {
            _rover.MoveForward();
        }

        // ASSERT
        callCount.Should().Be(forwardCount);
    }

    [Fact]
    public void MoveForward_Should_Pass_Correct_Args_To_Probe_Function()
    {
        // ARRANGE
        var initialLocation = _rover.CurrentLocation;
        var newLocation = initialLocation.Forward();

        var actualLocation = _rover.CurrentLocation;
        _rover.Initialise(initialLocation, location =>
        {
            actualLocation = location;
            return true;
        });

        // ACT
        _rover.MoveForward();

        // ASSERT
        actualLocation.Should().Be(newLocation);
    }

    [Fact]
    public void MoveForward_Should_Pass_Correct_EventArgs_If_Probe_Function_Returns_False()
    {
        // ARRANGE
        var initialLocation = _rover.CurrentLocation;

        var actualArgs = null as CommandExecutionEventArgs;

        _rover.Initialise(initialLocation, location => false);
        _rover.OnCommandExecution += (sender, args) => { actualArgs = args; };

        // ACT
        _rover.MoveForward();

        // ASSERT
        actualArgs.Should().NotBeNull();
        actualArgs?.Success.Should().BeFalse();
        actualArgs?.ExecutedCommand.Should().Be(Command.M);
        actualArgs?.Previous.Should().Be(initialLocation);
        actualArgs?.Current.Should().Be(initialLocation);
    }

    [Theory]
    [InlineData(0, 0, Direction.E, Command.R)]
    [InlineData(0, 0, Direction.W, Command.L)]
    [InlineData(0, 1, Direction.N, Command.M)]
    [InlineData(1, 1, Direction.N, Command.R, Command.M, Command.L, Command.M)]
    [InlineData(0, 0, Direction.S, Command.L, Command.L)]
    [InlineData(0, 0, Direction.S, Command.R, Command.R)]
    [InlineData(0, 0, Direction.W, Command.R, Command.R, Command.R)]
    [InlineData(0, -2, Direction.S, Command.L, Command.L, Command.M, Command.M)]
    [InlineData(-3, 0, Direction.E, Command.R, Command.R, Command.R, Command.M, Command.M, Command.M, Command.M, Command.L, Command.L, Command.M)]
    public void ExecuteCommands_Should_Update_Rover_Correctly(int expectedX, int expectedY, Direction expectedOrientation, params Command[] commands)
    {
        // ARRANGE
        var expectedLocation = new Location(expectedX, expectedY, expectedOrientation);

        // ACT
        _rover.ExecuteCommands(commands);

        // ASSERT
        _rover.CurrentLocation.Should().Be(expectedLocation);
    }
}