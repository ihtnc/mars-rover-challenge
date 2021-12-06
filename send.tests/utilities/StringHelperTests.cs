using System.Collections.Generic;
using MarsRoverChallenge.Send.Utilities;
using Xunit;
using FluentAssertions;

namespace MarsRoverChallenge.Send.Tests.Utilities;

public class StringHelperTests
{
    [Fact]
    public void GenerateShortId_Should_Be_Unique()
    {
        // ARRANGE
        var itemCount = 1000;
        var uniques = new HashSet<string>();

        for(var i = 0; i < itemCount; i++)
        {
            // ACT
            var id = StringHelper.GenerateShortId();

            // ASSERT
            uniques.Contains(id).Should().BeFalse();
            uniques.Add(id);
        }
    }
}