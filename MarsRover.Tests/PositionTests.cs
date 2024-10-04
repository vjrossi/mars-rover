using MarsRover;

namespace MarsRover.Tests;

public class PositionTests
{
    [Fact]
    public void Position_ShouldBeCreatedWithDefaultValues()
    {
        Position position = new();
        Assert.Equal(0, position.X);
        Assert.Equal(0, position.Y);
    }

    [Fact]
    public void Position_ShouldBeCreatedWithCustomValues()
    {
        Position position = new(10, 20);
        Assert.Equal(10, position.X);
        Assert.Equal(20, position.Y);
    }

    [Fact]
    public void Position_ShouldReturnValuesSet()
    {
        Position position = new(10, 20);
        Assert.Equal(10, position.X);
        Assert.Equal(20, position.Y);
    }
}


