using System;
using System.Text;
using System.Linq;
using MarsRoverChallenge.Send.Models;
using MarsRoverChallenge.Send.Utilities;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Apps.Console.Tests;

public class InputHelperTests
{
    [Fact]
    public void ParseCoordinate_Should_Parse_Landscape_Details()
    {
        // ARRANGE
        var x = 123;
        var y = 456;
        var bottomLeftCorner = new Coordinate(0, 0);
        var upperRightCorner = new Coordinate(x, y);

        var value = new StringBuilder();
        value.AppendLine($"{x} {y}");
        value.AppendLine("1 1 E");
        value.AppendLine("L");

        // ACT
        var actual = InputHelper.ParseInput($"{value}");

        // ASSERT
        actual.LandscapeDetails.BottomLeftCorner.Should().Be(bottomLeftCorner);
        actual.LandscapeDetails.UpperRightCorner.Should().Be(upperRightCorner);
    }

    [Fact]
    public void ParseCoordinate_Should_Parse_Rover_Starting_Location()
    {
        // ARRANGE
        var x = 111;
        var y = 222;
        var o = Direction.S;
        var startingLocation = new Location(x, y, o);

        var value = new StringBuilder();
        value.AppendLine("123 456");
        value.AppendLine($"{x} {y} {o}");
        value.AppendLine("L");

        // ACT
        var actual = InputHelper.ParseInput($"{value}");

        // ASSERT
        var command = actual.RoverCommands.Single();
        command.StartingLocation.Should().Be(startingLocation);
    }

    [Fact]
    public void ParseCoordinate_Should_Add_Rover_Commands_Appropriately()
    {
        // ARRANGE
        var commandCount = 3;

        var value = new StringBuilder();
        value.AppendLine("123 456");
        for(var i = 0; i < commandCount; i++)
        {
            value.AppendLine($"{i} 0 N");
            value.AppendLine("L");
        }

        // ACT
        var actual = InputHelper.ParseInput($"{value}");

        // ASSERT
        actual.RoverCommands.Should().HaveCount(commandCount);
        var list = actual.RoverCommands.ToArray();
        for(var i = 0; i < list.Length; i++)
        {
            var command = list[i];
            var expected = new Location(i, 0, Direction.N);
            command.StartingLocation.Should().Be(expected);
            command.Commands.Should().HaveCount(1);
        }
    }

    [Theory]
    [InlineData("L", Command.L)]
    [InlineData("LRLR", Command.L, Command.R, Command.L, Command.R)]
    [InlineData("MM", Command.M, Command.M)]
    [InlineData("LMRMMLLLMMMR", Command.L, Command.M, Command.R, Command.M, Command.M, Command.L, Command.L, Command.L, Command.M, Command.M, Command.M, Command.R)]
    public void ParseCoordinate_Should_Parse_Rover_Commands(string command, params Command[] expected)
    {
        // ARRANGE
        var value = new StringBuilder();
        value.AppendLine("123 456");
        value.AppendLine("11 22 W");
        value.AppendLine($"{command}");

        // ACT
        var actual = InputHelper.ParseInput($"{value}");

        // ASSERT
        var roverCommand = actual.RoverCommands.Single();
        roverCommand.Commands.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("1 1")]
    [InlineData("1 1\r0 0 E")]
    [InlineData("1 1\n0 0 E\rM\r\nL")]
    public void ParseCoordinate_Should_Handle_Invalid_Content(string content)
    {
        // ACT
        var action = () => InputHelper.ParseInput(content);

        // ASSERT
        action.Should().Throw<FormatException>().WithMessage("Unrecognised file content.");
    }

    [Theory]
    [InlineData("123")]
    [InlineData("L")]
    [InlineData("123 456 E")]
    [InlineData("Invalid")]
    public void ParseCoordinate_Should_Handle_Invalid_Landscape_Details(string value)
    {
        // ARRANGE
        var input = new StringBuilder();
        input.AppendLine(value);
        input.AppendLine("1 1 E");
        input.AppendLine("L");

        // ACT
        var action = () => InputHelper.ParseInput($"{input}");

        // ASSERT
        action.Should().Throw<FormatException>().WithMessage("Unrecognised format found while reading the coordinate data.");
    }

    [Theory]
    [InlineData("123")]
    [InlineData("L")]
    [InlineData("123 456 789")]
    [InlineData("123 456")]
    [InlineData("Invalid")]
    public void ParseCoordinate_Should_Handle_Invalid_Initial_Location_Data(string value)
    {
        // ARRANGE
        var input = new StringBuilder();
        input.AppendLine("1 1");
        input.AppendLine(value);
        input.AppendLine("L");

        // ACT
        var action = () => InputHelper.ParseInput($"{input}");

        // ASSERT
        action.Should().Throw<FormatException>().WithMessage("Unrecognised format found while reading the location data.");
    }

    [Theory]
    [InlineData("123 456")]
    [InlineData("123 456 S")]
    [InlineData("X")]
    [InlineData("N")]
    [InlineData("Invalid")]
    public void ParseCoordinate_Should_Handle_Invalid_Command(string value)
    {
        // ARRANGE
        var input = new StringBuilder();
        input.AppendLine("1 1");
        input.AppendLine("1 1 N");
        input.AppendLine(value);

        // ACT
        var action = () => InputHelper.ParseInput($"{input}");

        // ASSERT
        action.Should().Throw<FormatException>().WithMessage("Unrecognised format found while reading the command data.");
    }
}