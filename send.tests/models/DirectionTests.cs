using System;
using System.ComponentModel;
using System.Linq;
using MarsRoverChallenge.Send.Models;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Models;

public class DirectionTests
{
    [Theory]
    [InlineData(Direction.N, "Represents a north-facing direction")]
    [InlineData(Direction.E, "Represents an east-facing direction")]
    [InlineData(Direction.S, "Represents a south-facing direction")]
    [InlineData(Direction.W, "Represents a west-facing direction")]
    public void Direction_Should_Have_Appropriate_Description(Direction direction, string expected)
    {
        // ACT
        var enumType = direction.GetType();
        var enumValue = Enum.GetName(enumType, direction) ?? string.Empty;
        var attributes = enumType.GetField(enumValue)?.GetCustomAttributes(false).OfType<DescriptionAttribute>() ?? new DescriptionAttribute[0];

        // ASSERT
        attributes.Should().NotBeNullOrEmpty();
        attributes.Should().HaveCount(1);

        var description = attributes.First();
        description.Description.Should().Be(expected);
    }
}