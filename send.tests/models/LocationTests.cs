using System;
using MarsRoverChallenge.Send.Models;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Models;

public class LocationTests
{
    [Fact]
    public void Default_Constructor_Should_Set_Properties_Correctly()
    {
        // ARRANGE
        var defaultX = 0;
        var defaultY = 0;
        var defaultOrientation = Direction.N;

        // ACT
        var location = new Location();

        // ASSERT
        location.Position.X.Should().Be(defaultX);
        location.Position.Y.Should().Be(defaultY);
        location.Orientation.Should().Be(defaultOrientation);
    }

    [Fact]
    public void Constructor1_Should_Set_Properties_Correctly()
    {
        // ARRANGE
        var x = 123;
        var y = 456;
        var orientation = Direction.W;

        // ACT
        var location = new Location(x, y, orientation);

        // ASSERT
        location.Position.X.Should().Be(x);
        location.Position.Y.Should().Be(y);
        location.Orientation.Should().Be(orientation);
    }

    [Fact]
    public void Constructor2_Should_Set_Properties_Correctly()
    {
        // ARRANGE
        var position = new Coordinate(123, 456);
        var orientation = Direction.W;

        // ACT
        var location = new Location(position, orientation);

        // ASSERT
        location.Position.Should().Be(position);
        location.Orientation.Should().Be(orientation);
    }

    [Theory]
    [InlineData(0, 0, Direction.N, "0 0 N")]
    [InlineData(123, -456, Direction.E, "123 -456 E")]
    [InlineData(-135, 246, Direction.S, "-135 246 S")]
    [InlineData(111, 222, Direction.W, "111 222 W")]
    public void ToString_Should_Return_Correctly(int x, int y, Direction orientation, string expected)
    {
        // ARRANGE
        var location = new Location(x, y, orientation);

        // ACT
        var actual = $"{location}";

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("123 456")]
    [InlineData("123 456 A")]
    [InlineData("123 456 NE")]
    [InlineData("123 456 789")]
    [InlineData("123 456 E 789")]
    [InlineData("123 asd E")]
    [InlineData("asd 123 W")]
    [InlineData("asd qwe N")]
    [InlineData("12.3 4.56 N")]
    [InlineData("123.456.679 111.222.333 E")]
    [InlineData("1,234 5,678 E")]
    [InlineData("12-3 4-56 W")]
    public void TryParse_Should_Handle_Invalid_Values(string value)
    {
        // ACT
        var actual = Location.TryParse(value, out var parsed);

        // ASSERT
        actual.Should().BeFalse();
        parsed.Should().Be(default(Location));
    }

    [Theory]
    [InlineData("123 456 N", 123, 456, Direction.N)]
    [InlineData("   -123 456 S", -123, 456, Direction.S)]
    [InlineData("123   -456 E", 123, -456, Direction.E)]
    [InlineData("123 456   W", 123, 456, Direction.W)]
    [InlineData("135 246 S   ", 135, 246, Direction.S)]
    public void TryParse_Should_Handle_Valid_Values(string value, int expectedX, int expectedY, Direction expectedOrientation)
    {
        // ACT
        var actual = Location.TryParse(value, out var parsed);

        // ASSERT
        actual.Should().BeTrue();
        parsed.Position.X.Should().Be(expectedX);
        parsed.Position.Y.Should().Be(expectedY);
        parsed.Orientation.Should().Be(expectedOrientation);
    }

    [Theory]
    [InlineData("123 456 E")]
    [InlineData(123)]
    [InlineData(true)]
    public void Object_Equals_Should_Handle_Invalid_Values_Properly(object other)
    {
        // ARRANGE
        var value = new Location(123, 456, Direction.N);

        // ACT
        var actual = value.Equals(other);

        // ASSERT
        actual.Should().BeFalse();
    }

    [Fact]
    public void Object_Equals_Should_Handle_Null_Values_Properly()
    {
        // ARRANGE
        var value = new Location(123, 456, Direction.N);

        // ACT
        var actual = value.Equals(null);

        // ASSERT
        actual.Should().BeFalse();
    }

    [Theory]
    [InlineData(123, 456, Direction.N, true)]
    [InlineData(123, 456, Direction.E, false)]
    [InlineData(123, -456, Direction.N, false)]
    [InlineData(-123, 456, Direction.N, false)]
    public void Object_Equals_Should_Handle_Location_Values_Properly(int x, int y, Direction orientation, bool expected)
    {
        // ARRANGE
        var value = new Location(123, 456, Direction.N);
        object other = new Location(x, y, orientation);

        // ACT
        var actual = value.Equals(other);

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(123, 456, Direction.N, true)]
    [InlineData(123, 456, Direction.S, false)]
    [InlineData(123, -456, Direction.N, false)]
    [InlineData(-123, 456, Direction.N, false)]
    public void Coordinate_Equals_Should_Handle_Coordinate_Values_Properly(int x, int y, Direction orientation, bool expected)
    {
        // ARRANGE
        var value = new Location(123, 456, Direction.N);
        var other = new Location(x, y, orientation);

        // ACT
        var actual = value.Equals(other);

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(123, 456, Direction.N, true)]
    [InlineData(123, 456, Direction.S, false)]
    [InlineData(123, -456, Direction.N, false)]
    [InlineData(-123, 456, Direction.N, false)]
    public void Equality_Operator_Should_Return_Properly(int x, int y, Direction orientation, bool expected)
    {
        // ARRANGE
        var value = new Location(123, 456, Direction.N);
        var other = new Location(x, y, orientation);

        // ACT
        var actual = value == other;

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(123, 456, Direction.N, false)]
    [InlineData(123, 456, Direction.S, true)]
    [InlineData(123, -456, Direction.N, true)]
    [InlineData(-123, 456, Direction.N, true)]
    public void Inequality_Operator_Should_Return_Properly(int x, int y, Direction orientation, bool expected)
    {
        // ARRANGE
        var value = new Location(123, 456, Direction.N);
        var other = new Location(x, y, orientation);

        // ACT
        var actual = value != other;

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(123, 456, Direction.N)]
    [InlineData(123, -456, Direction.W)]
    [InlineData(-123, 456, Direction.S)]
    public void GetHashCode_Should_Return_Properly(int x, int y, Direction orientation)
    {
        // ARRANGE
        var value = new Location(x, y, orientation);
        var expected = HashCode.Combine(value.Position, value.Orientation);

        // ACT
        var actual = value.GetHashCode();

        // ASSERT
        actual.Should().Be(expected);
    }
}