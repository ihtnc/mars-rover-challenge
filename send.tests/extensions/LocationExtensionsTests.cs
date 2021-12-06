using MarsRoverChallenge.Send.Extensions;
using MarsRoverChallenge.Send.Models;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Extensions;

public class LocationExtensionsTests
{
    [Theory]
    [InlineData(false, 0, 0, Direction.N, 0, 1)]
    [InlineData(false, 0, 0, Direction.E, 1, 0)]
    [InlineData(false, 0, 0, Direction.S, 0, -1)]
    [InlineData(false, 0, 0, Direction.W, -1, 0)]
    [InlineData(true, 0, 0, Direction.N, 0, -1)]
    [InlineData(true, 0, 0, Direction.E, -1, 0)]
    [InlineData(true, 0, 0, Direction.S, 0, 1)]
    [InlineData(true, 0, 0, Direction.W, 1, 0)]
    public void Forward_Should_Return_Correctly(bool reverse, int x, int y, Direction orientation, int expectedX, int expectedY)
    {
        // ARRANGE
        var initial = new Location(x, y, orientation);

        // ACT
        var actual = initial.Forward(reverse);

        // ASSERT
        var expected = new Coordinate(expectedX, expectedY);
        actual.Position.Should().Be(expected);
        actual.Orientation.Should().Be(initial.Orientation);
    }
}