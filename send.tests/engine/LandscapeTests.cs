using MarsRoverChallenge.Send.Engine;
using MarsRoverChallenge.Send.Models;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Engine;

public class LandscapeTests
{
    private readonly ILandscape _landscape;

    public LandscapeTests()
    {
        var bottomLeftCorner = new Coordinate(0, 0);
        var upperRightCorner = new Coordinate(1, 1);

        _landscape = new Landscape(bottomLeftCorner, upperRightCorner);
    }

    [Fact]
    public void Constructor_Should_Set_Corners_Correctly()
    {
        // ARRANGE
        var bottomLeftCorner = new Coordinate(12, 34);
        var upperRightCorner = new Coordinate(56, 78);
        var expectedBottomLeftCorner = bottomLeftCorner;
        var expectedUpperRightCorner = upperRightCorner;

        // ACT
        var landscape = new Landscape(bottomLeftCorner, upperRightCorner);

        // ASSERT
        landscape.BottomLeftCorner.Should().Be(expectedBottomLeftCorner);
        landscape.UpperRightCorner.Should().Be(expectedUpperRightCorner);
    }

    [Fact]
    public void Initialise_Should_Set_Corners_Correctly()
    {
        // ARRANGE
        var bottomLeftCorner = new Coordinate(12, 34);
        var upperRightCorner = new Coordinate(56, 78);
        var expectedBottomLeftCorner = bottomLeftCorner;
        var expectedUpperRightCorner = upperRightCorner;

        // ACT
        _landscape.Initialise(bottomLeftCorner, upperRightCorner);

        // ASSERT
        _landscape.BottomLeftCorner.Should().Be(expectedBottomLeftCorner);
        _landscape.UpperRightCorner.Should().Be(expectedUpperRightCorner);
    }

    [Fact]
    public void IsOccupied_Should_Return_Correctly_On_Initialisation()
    {
        // ARRANGE
        var bottomLeftCorner = _landscape.BottomLeftCorner;
        var upperRightCorner = _landscape.UpperRightCorner;

        for(var x = bottomLeftCorner.X; x < upperRightCorner.X; x++)
        {
            for(var y = bottomLeftCorner.Y; y < upperRightCorner.Y; y++)
            {
                var coordinate = new Coordinate(x, y);

                // ACT
                var isOccupied = _landscape.IsOccupied(coordinate);

                // ASSERT
                isOccupied.Should().BeFalse();
            }
        }
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(0, -1)]
    [InlineData(-1, 0)]
    [InlineData(2, 2)]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    [InlineData(-1, 1)]
    [InlineData(-1, 2)]
    [InlineData(0, 2)]
    [InlineData(2, -1)]
    [InlineData(2, 0)]
    [InlineData(1, -1)]
    [InlineData(3, 3)]
    [InlineData(-123, -456)]
    public void IsOccupied_Should_Handle_Out_Of_Bounds_Values(int x, int y)
    {
        // ARRANGE
        var location = new Coordinate(x, y);

        // ACT
        var actual = _landscape.IsOccupied(location);

        // ASSERT
        actual.Should().BeNull();
    }

    [Theory]
    [InlineData(0, 1, true)]
    [InlineData(0, 0, false)]
    [InlineData(1, 1, false)]
    [InlineData(1, 0, false)]
    public void IsOccupied_Should_Return_Correctly(int x, int y, bool expected)
    {
        // ARRANGE
        var location = new Coordinate(0, 1);
        _landscape.AddMarker(location);

        // ACT
        var check = new Coordinate(x, y);
        var actual = _landscape.IsOccupied(check);

        // ASSERT
        actual.Should().Be(expected);
    }

    [Fact]
    public void AddMarker_Should_Update_Landscape_Correctly()
    {
        // ARRANGE
        var location = new Coordinate(1, 1);

        // ACT
        var success = _landscape.AddMarker(location);

        // ARRANGE
        success.Should().BeTrue();

        var expected = _landscape.IsOccupied(location);
        expected.Should().BeTrue();
    }

    [Fact]
    public void AddMarker_Should_Handle_Out_Of_Bounds_Value()
    {
        // ARRANGE
        var location = new Coordinate(-3, -3);

        // ACT
        var success = _landscape.AddMarker(location);

        // ARRANGE
        success.Should().BeFalse();
    }

    [Fact]
    public void RemoveMarker_Should_Update_Landscape_Correctly()
    {
        // ARRANGE
        var location = new Coordinate(1, 0);
        _landscape.AddMarker(location);

        // ACT
        var success = _landscape.RemoveMarker(location);

        // ARRANGE
        success.Should().BeTrue();

        var expected = _landscape.IsOccupied(location);
        expected.Should().BeFalse();
    }

    [Fact]
    public void RemoveMarker_Should_Handle_Out_Of_Bounds_Value()
    {
        // ARRANGE
        var location = new Coordinate(1, 2);

        // ACT
        var success = _landscape.RemoveMarker(location);

        // ARRANGE
        success.Should().BeFalse();
    }

    [Fact]
    public void MoveMarker_Should_Update_Landscape_Correctly()
    {
        // ARRANGE
        var initialLocation = new Coordinate(0, 0);
        _landscape.AddMarker(initialLocation);

        var newLocation = new Coordinate(1, 1);

        // ACT
        var success = _landscape.MoveMarker(initialLocation, newLocation);

        // ARRANGE
        success.Should().BeTrue();

        var actualInitial = _landscape.IsOccupied(initialLocation);
        actualInitial.Should().BeFalse();

        var actualNew = _landscape.IsOccupied(newLocation);
        actualNew.Should().BeTrue();
    }

    [Fact]
    public void MoveMarker_Should_Handle_Out_Of_Bounds_Initial_Location()
    {
        // ARRANGE
        var initialLocation = new Coordinate(-1, -1);
        _landscape.AddMarker(initialLocation);

        var newLocation = new Coordinate(1, 1);

        // ACT
        var success = _landscape.MoveMarker(initialLocation, newLocation);

        // ARRANGE
        success.Should().BeFalse();

        var actualInitial = _landscape.IsOccupied(newLocation);
        actualInitial.Should().BeFalse();
    }

    [Fact]
    public void MoveMarker_Should_Handle_Out_Of_Bounds_New_Location()
    {
        // ARRANGE
        var initialLocation = new Coordinate(0, 0);
        _landscape.AddMarker(initialLocation);

        var newLocation = new Coordinate(1, -1);

        // ACT
        var success = _landscape.MoveMarker(initialLocation, newLocation);

        // ARRANGE
        success.Should().BeFalse();

        var actualInitial = _landscape.IsOccupied(initialLocation);
        actualInitial.Should().BeTrue();
    }
}