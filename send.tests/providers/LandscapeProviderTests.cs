using MarsRoverChallenge.Send.Models;
using MarsRoverChallenge.Send.Providers;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Providers;

public class LandscapeProviderTests
{
    [Fact]
    public void CreateLandscape_Should_Create_New_Landscape_Instance()
    {
        // ARRANGE
        var provider = new LandscapeProvider();
        var coordinate1 = new Coordinate(12, 34);
        var coordinate2 = new Coordinate(123, 456);
        var expectedLowerLeftCorner = coordinate1;
        var expectedUpperRightCorner = coordinate2;

        // ACT
        var actual = provider.CreateLandscape(expectedLowerLeftCorner, expectedUpperRightCorner);

        // ASSERT
        actual.Should().NotBeNull();
        actual.BottomLeftCorner.Should().Be(expectedLowerLeftCorner);
        actual.UpperRightCorner.Should().Be(expectedUpperRightCorner);
    }

    [Fact]
    public void CreateLandscape_Should_Always_Create_New_Landscape_Instance()
    {
        // ARRANGE
        var provider = new LandscapeProvider();
        var coordinate1 = new Coordinate(0, 0);
        var coordinate2 = new Coordinate(123, 456);

        // ACT
        var initial = provider.CreateLandscape(coordinate1, coordinate2);
        var actual = provider.CreateLandscape(coordinate1, coordinate2);

        // ASSERT
        actual.Should().NotBeSameAs(initial);
    }
}