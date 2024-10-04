using MarsRover;

namespace MarsRover.Tests;

public class NasaTests
{
    [Fact]
    public void Nasa_ShouldAcceptInitialisationCommand()
    {
        // Arrange
        Nasa nasa = new();

        // Act
        var result = nasa.AcceptCommands(["100 100"]);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Nasa_ShouldDisallowRoverGoingOffGrid()
    {
        // Arrange
        Nasa nasa = new();
        var result = nasa.AcceptCommands(["5 5"]);

        // Act
        result = nasa.AcceptCommands(["5 5", "5 5 N"]);
    }

    [Fact]
    public void Nasa_ShouldNotAllowCommandsBeforeInitialisation()
    {
        // Arrange
        Nasa nasa = new();

        // Act & Assert
        Assert.Throws<Exception>(() => nasa.AcceptCommands(["M"]));
    }

    // test multiple rovers
    [Fact]
    public void Nasa_ShouldAcceptMultipleRovers()
    {
        // Arrange
        Nasa nasa = new();
        nasa.AcceptCommands(["100 100"]);

        // Act
        var result1 = nasa.AcceptCommands(["5 5 S", "MRMMRMLMML"]);
        var result2 = nasa.AcceptCommands(["55 55 E", "MMLMMLMM"]);
        var result3 = nasa.AcceptCommands(["0 0 N", "MMRMMLM"]);
        var result4 = nasa.AcceptCommands(["10 10 W", "MLMLMLMRM"]);
        var result5 = nasa.AcceptCommands(["99 99 S", "MLLMMRMRM"]);

        // Assert
        Assert.Equal("1 5 S", result1);
        Assert.Equal("55 57 W", result2);
        Assert.Equal("2 3 N", result3);
        Assert.Equal("11 10 E", result4);
        Assert.Equal("99 99 N", result5);
    }
}

