using System;
using MarsRoverChallenge.Send.Models;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Models;

public class CoordinateTests
{
    [Fact]
    public void Default_Constructor_Should_Set_Properties_Correctly()
    {
        // ARRANGE
        var defaultX = 0;
        var defaultY = 0;

        // ACT
        var coordinate = new Coordinate();

        // ASSERT
        coordinate.X.Should().Be(defaultX);
        coordinate.Y.Should().Be(defaultY);
    }

    [Fact]
    public void Constructor_Should_Set_Properties_Correctly()
    {
        // ARRANGE
        var x = 123;
        var y = 456;

        // ACT
        var coordinate = new Coordinate(x, y);

        // ASSERT
        coordinate.X.Should().Be(x);
        coordinate.Y.Should().Be(y);
    }

    [Theory]
    [InlineData(0, 0, "0 0")]
    [InlineData(123, -456, "123 -456")]
    [InlineData(-135, 246, "-135 246")]
    public void ToString_Should_Return_Correctly(int x, int y, string expected)
    {
        // ARRANGE
        var coordinate = new Coordinate(x, y);

        // ACT
        var actual = $"{coordinate}";

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("123 456 789")]
    [InlineData("asd")]
    [InlineData("123 asd 456")]
    [InlineData("asd qwe")]
    [InlineData("12.3 4.56")]
    [InlineData("123.456.679 111.222.333")]
    [InlineData("1,234 5,678")]
    [InlineData("12-3 4-56")]
    public void TryParse_Should_Handle_Invalid_Values(string value)
    {
        // ACT
        var actual = Coordinate.TryParse(value, out var parsed);

        // ASSERT
        actual.Should().BeFalse();
        parsed.Should().Be(default(Coordinate));
    }

    [Theory]
    [InlineData("123 456", 123, 456)]
    [InlineData("   -123 456", -123, 456)]
    [InlineData("123   -456", 123, -456)]
    [InlineData("123 456   ", 123, 456)]
    public void TryParse_Should_Handle_Valid_Values(string value, int expectedX, int expectedY)
    {
        // ACT
        var actual = Coordinate.TryParse(value, out var parsed);

        // ASSERT
        actual.Should().BeTrue();
        parsed.X.Should().Be(expectedX);
        parsed.Y.Should().Be(expectedY);
    }

    [Theory]
    [InlineData("123 456")]
    [InlineData(123)]
    [InlineData(true)]
    public void Object_Equals_Should_Handle_Invalid_Values_Properly(object other)
    {
        // ARRANGE
        var value = new Coordinate(123, 456);

        // ACT
        var actual = value.Equals(other);

        // ASSERT
        actual.Should().BeFalse();
    }

    [Fact]
    public void Object_Equals_Should_Handle_Null_Values_Properly()
    {
        // ARRANGE
        var value = new Coordinate(123, 456);

        // ACT
        var actual = value.Equals(null);

        // ASSERT
        actual.Should().BeFalse();
    }

    [Theory]
    [InlineData(123, 456, true)]
    [InlineData(123, -456, false)]
    [InlineData(-123, 456, false)]
    public void Object_Equals_Should_Handle_Coordinate_Values_Properly(int x, int y, bool expected)
    {
        // ARRANGE
        var value = new Coordinate(123, 456);
        object other = new Coordinate(x, y);

        // ACT
        var actual = value.Equals(other);

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(123, 456, true)]
    [InlineData(123, -456, false)]
    [InlineData(-123, 456, false)]
    public void Coordinate_Equals_Should_Handle_Coordinate_Values_Properly(int x, int y, bool expected)
    {
        // ARRANGE
        var value = new Coordinate(123, 456);
        var other = new Coordinate(x, y);

        // ACT
        var actual = value.Equals(other);

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(123, 456, true)]
    [InlineData(123, -456, false)]
    [InlineData(-123, 456, false)]
    public void Equality_Operator_Should_Return_Properly(int x, int y, bool expected)
    {
        // ARRANGE
        var value = new Coordinate(123, 456);
        var other = new Coordinate(x, y);

        // ACT
        var actual = value == other;

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(123, 456, false)]
    [InlineData(123, -456, true)]
    [InlineData(-123, 456, true)]
    public void Inequality_Operator_Should_Return_Properly(int x, int y, bool expected)
    {
        // ARRANGE
        var value = new Coordinate(123, 456);
        var other = new Coordinate(x, y);

        // ACT
        var actual = value != other;

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(123, 456)]
    [InlineData(123, -456)]
    [InlineData(-123, 456)]
    public void GetHashCode_Should_Return_Properly(int x, int y)
    {
        // ARRANGE
        var value = new Coordinate(x, y);
        var expected = HashCode.Combine(x, y);

        // ACT
        var actual = value.GetHashCode();

        // ASSERT
        actual.Should().Be(expected);
    }
}