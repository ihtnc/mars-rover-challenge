using MarsRoverChallenge.Send.Providers;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Providers;

public class RoverProviderTests
{
    [Fact]
    public void CreateRover_Should_Create_New_Rover_Instance()
    {
        // ARRANGE
        var provider = new RoverProvider();

        // ACT
        var actual = provider.CreateRover();

        // ASSERT
        actual.Should().NotBeNull();
    }

    [Fact]
    public void CreateRover_Should_Always_Create_New_Rover_Instance()
    {
        // ARRANGE
        var provider = new RoverProvider();

        // ACT
        var initial = provider.CreateRover();
        var actual = provider.CreateRover();

        // ASSERT
        actual.Should().NotBeSameAs(initial);
    }
}