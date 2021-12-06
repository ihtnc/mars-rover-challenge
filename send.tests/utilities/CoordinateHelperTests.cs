using MarsRoverChallenge.Send.Models;
using MarsRoverChallenge.Send.Utilities;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Utilities;

public class CoordinateHelperTests
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(123, 456)]
    public void ToMap_Should_Create_Map_With_Correct_Dimensions(int upperRightX, int upperRightY)
    {
        // ARRANGE
        var bottomLeftCorner = new Coordinate(0, 0);
        var upperRightCorner = new Coordinate(upperRightX, upperRightY);

        // ACT
        var actual = CoordinateHelper.ToMap<int>(bottomLeftCorner, upperRightCorner);

        // ASSERT
        actual.Rank.Should().Be(2);
    }

    [Theory]
    [InlineData(1, 1, 2, 2)]
    [InlineData(0, 0, 1, 1)]
    [InlineData(1, 2, 2, 3)]
    [InlineData(2, 1, 3, 2)]
    [InlineData(0, 1, 1, 2)]
    [InlineData(1, 0, 2, 1)]
    public void ToMap_Should_Create_Map_Based_On_Upper_Right_Corner(int upperRightX, int upperRightY, int expectedX, int expectedY)
    {
        // ARRANGE
        var bottomLeftCorner = new Coordinate(0, 0);
        var upperRightCorner = new Coordinate(upperRightX, upperRightY);

        // ACT
        var actual = CoordinateHelper.ToMap<int>(bottomLeftCorner, upperRightCorner);

        // ASSERT
        var actualX = actual.GetLength(0);
        actualX.Should().Be(expectedX);

        var actualY = actual.GetLength(1);
        actualY.Should().Be(expectedY);
    }

    [Theory]
    [InlineData(-1, -1, 2, 2)]
    [InlineData(0, 0, 1, 1)]
    [InlineData(-1, -2, 2, 3)]
    [InlineData(-2, -1, 3, 2)]
    [InlineData(0, -1, 1, 2)]
    [InlineData(-1, 0, 2, 1)]
    public void ToMap_Should_Create_Map_Based_On_Bottom_Left_Corner(int bottomLeftX, int bottomLeftY, int expectedX, int expectedY)
    {
        // ARRANGE
        var bottomLeftCorner = new Coordinate(bottomLeftX, bottomLeftY);
        var upperRightCorner = new Coordinate(0, 0);

        // ACT
        var actual = CoordinateHelper.ToMap<int>(bottomLeftCorner, upperRightCorner);

        // ASSERT
        var actualX = actual.GetLength(0);
        actualX.Should().Be(expectedX);

        var actualY = actual.GetLength(1);
        actualY.Should().Be(expectedY);
    }

    [Theory]
    [InlineData(-2, 1, -1, 2, 2, 2)]
    [InlineData(-1, -1, 1, 1, 3, 3)]
    [InlineData(2, 1, 2, 3, 1, 3)]
    [InlineData(-3, -1, -2, 1, 2, 3)]
    [InlineData(-1, 1, 1, 1, 3, 1)]
    [InlineData(2, -3, 2, 3, 1, 7)]
    [InlineData(-2, -1, -2, -1, 1, 1)]
    [InlineData(-1, -3, 1, -1, 3, 3)]
    [InlineData(2, -2, 3, -1, 2, 2)]
    public void ToMap_Should_Create_Map_Based_On_Both_Corners(int bottomLeftX, int bottomLeftY, int upperRightX, int upperRightY, int expectedX, int expectedY)
    {
        // ARRANGE
        var bottomLeftCorner = new Coordinate(bottomLeftX, bottomLeftY);
        var upperRightCorner = new Coordinate(upperRightX, upperRightY);

        // ACT
        var actual = CoordinateHelper.ToMap<int>(bottomLeftCorner, upperRightCorner);

        // ASSERT
        var actualX = actual.GetLength(0);
        actualX.Should().Be(expectedX);

        var actualY = actual.GetLength(1);
        actualY.Should().Be(expectedY);
    }

    [Theory]
    [InlineData(-1, 1, true)]
    [InlineData(0, 1, true)]
    [InlineData(1, 1, true)]
    [InlineData(1, 0, true)]
    [InlineData(1, -1, true)]
    [InlineData(0, -1, true)]
    [InlineData(-1, -1, true)]
    [InlineData(-1, 0, true)]
    [InlineData(0, 0, true)]
    [InlineData(-2, 2, false)]
    [InlineData(2, 2, false)]
    [InlineData(2, -2, false)]
    [InlineData(-2, -2, false)]
    [InlineData(-1, 2, false)]
    [InlineData(1, 2, false)]
    [InlineData(2, 1, false)]
    [InlineData(2, -1, false)]
    [InlineData(1, -2, false)]
    [InlineData(-1, -2, false)]
    [InlineData(-2, -1, false)]
    [InlineData(-2, 1, false)]
    public void IsWithinBounds_Should_Return_Correctly(int locationX, int locationY, bool expected)
    {
        // ARRANGE
        var bottomLeftCorner = new Coordinate(-1, -1);
        var upperRightCorner = new Coordinate(1, 1);
        var location = new Coordinate(locationX, locationY);

        // ACT
        var actual = CoordinateHelper.IsWithinBounds(bottomLeftCorner, upperRightCorner, location);

        // ASSERT
        actual.Should().Be(expected);
    }
}