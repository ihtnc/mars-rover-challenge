using MarsRoverChallenge.Send.Extensions;
using MarsRoverChallenge.Send.Models;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Extensions;

public class DirectionExtensionsTests
{
    [Theory]
    [InlineData(false, Direction.N, Direction.E)]
    [InlineData(false, Direction.E, Direction.S)]
    [InlineData(false, Direction.S, Direction.W)]
    [InlineData(false, Direction.W, Direction.N)]
    [InlineData(true, Direction.N, Direction.W)]
    [InlineData(true, Direction.E, Direction.N)]
    [InlineData(true, Direction.S, Direction.E)]
    [InlineData(true, Direction.W, Direction.S)]
    public void Turn_Should_Return_Correctly(bool reverse, Direction initial, Direction expected)
    {
        // ACT
        var actual = initial.Turn(reverse);

        // ASSERT
        actual.Should().Be(expected);
    }
}