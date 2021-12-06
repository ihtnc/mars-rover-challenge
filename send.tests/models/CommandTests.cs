using System;
using System.ComponentModel;
using System.Linq;
using MarsRoverChallenge.Send.Models;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Models;

public class CommandTests
{
    [Theory]
    [InlineData(Command.L, "Represents the command for turning left")]
    [InlineData(Command.R, "Represents the command for turning right")]
    [InlineData(Command.M, "Represents the command for moving forward")]
    public void Command_Should_Have_Appropriate_Description(Command command, string expected)
    {
        // ACT
        var enumType = command.GetType();
        var enumValue = Enum.GetName(enumType, command) ?? string.Empty;
        var attributes = enumType.GetField(enumValue)?.GetCustomAttributes(false).OfType<DescriptionAttribute>() ?? new DescriptionAttribute[0];

        // ASSERT
        attributes.Should().NotBeNullOrEmpty();
        attributes.Should().HaveCount(1);

        var description = attributes.First();
        description.Description.Should().Be(expected);
    }
}