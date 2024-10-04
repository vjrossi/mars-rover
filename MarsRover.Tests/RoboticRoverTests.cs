namespace MarsRover.Tests;

public class RoboticRoverTests
{
    private Nasa _nasa;

    public RoboticRoverTests()
    {
        _nasa = new Nasa();
    }

    [Fact]
    public void RoboticRover_ShouldBeCreated()
    {
        // Act
        RoboticRover rover = new(new Position(0, 0), Facing.N, new RoverCamera());

        // Assert
        Assert.NotNull(rover);
    }

    [Theory]
    [InlineData(Facing.N, 5, 6)]
    [InlineData(Facing.E, 6, 5)]
    [InlineData(Facing.S, 5, 4)]
    [InlineData(Facing.W, 4, 5)]
    public void RoboticRover_ShouldMoveOneUnitInFacingDirection(Facing facing, int expectedX, int expectedY)
    {
        // Arrange
        RoboticRover rover = new(new Position(5, 5), facing, new RoverCamera());

        // Act
        rover.Move(1);

        // Assert value equality
        Assert.Equal(new Position(expectedX, expectedY), rover.RoverPosition);
    }

    [Fact]
    public void RoboticRover_ShouldRotateLeft()
    {
        // Arrange
        RoboticRover rover = new(new Position(0, 0), Facing.N, new RoverCamera());

        // Act
        rover.Rotate(TurnDirection.L);

        // Assert
        Assert.Equal(Facing.W, rover.RoverFacing);
    }

    [Fact]
    public void RoboticRover_ShouldRotateRight()
    {
        // Arrange
        RoboticRover rover = new(new Position(0, 0), Facing.N, new RoverCamera());

        // Act
        rover.Rotate(TurnDirection.R);

        // Assert
        Assert.Equal(Facing.E, rover.RoverFacing);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(100)]
    public void RoboticRover_ShouldNotMoveGreaterThanMaxSpeed(int units)
    {
        // Arrange
        RoboticRover rover = new(new Position(5, 5), Facing.N, new RoverCamera());

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => rover.Move(units));
    }

    [Theory]
    [InlineData(-1)]
    public void RoboticRover_ShouldNotMoveIfLessThanOne(int units)
    {
        // Arrange
        RoboticRover rover = new(new Position(5, 5), Facing.N, new RoverCamera());

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => rover.Move(units));
    }

    [Fact]
    public void RoboticRover_ShouldAcceptAndActOnMoveCommand()
    {
        // Arrange
        RoboticRover rover = new(new Position(0, 0), Facing.N, new RoverCamera());

        // Act
        rover.ExecuteCommands([new MoveCommand(rover, 1)]);

        // Assert
        Assert.Equal(new Position(0, 1), rover.RoverPosition);
    }

    [Fact]
    public void RoboticRover_ShouldAcceptAndActOnRotateCommand()
    {
        // Arrange
        RoboticRover rover = new(new Position(0, 0), Facing.N, new RoverCamera());

        // Act
        rover.ExecuteCommands([new RotateCommand(rover, TurnDirection.R)]);

        // Assert
        Assert.Equal(Facing.E, rover.RoverFacing);
    }

    [Theory]
    [InlineData("M", 5, 6, Facing.N)]
    [InlineData("R", 5, 5, Facing.E)]
    [InlineData("L", 5, 5, Facing.W)]
    [InlineData("MR", 5, 6, Facing.E)]
    [InlineData("ML", 5, 6, Facing.W)]
    [InlineData("RM", 6, 5, Facing.E)]
    [InlineData("RRM", 5, 4, Facing.S)]
    [InlineData("RRRM", 4, 5, Facing.W)]
    [InlineData("RRRRM", 5, 6, Facing.N)]
    [InlineData("MMRMMRMRRM", 7, 7, Facing.N)]
    public void RoboticRover_ShouldAcceptAndActOnMultipleCommands(string commandString, int expectedX, int expectedY, Facing expectedFacing)
    {
        // Arrange
        RoboticRover rover = new(new Position(5, 5), Facing.N, new RoverCamera());
        List<ICommand> commands = new RoverCommandParser().GetCommands(commandString, rover);

        // Act
        string result = rover.ExecuteCommands(commands);

        // Assert
        var expectedResult = $"{expectedX} {expectedY} {expectedFacing}";
        Assert.Equal(expectedResult, result);
    }
}