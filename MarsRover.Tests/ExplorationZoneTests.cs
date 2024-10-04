using MarsRover.Core.Models;

namespace MarsRover.Tests;

public class ExplorationZoneTests
{
    [Fact]
    public void ExplorationZone_ShouldBeCreated()
    {
        ExplorationZone explorationZone = new ExplorationZone(10, 10);
        Assert.NotNull(explorationZone);
    }

    [Fact]
    public void ExplorationZone_ShouldNotBeCreatedWithLessThanOneSpace()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new ExplorationZone(0, 0));
    }

    [Fact]
    public void ExplorationZone_ShouldNotBeCreatedWithNegativeValues()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new ExplorationZone(-1, -1));
    }
}

