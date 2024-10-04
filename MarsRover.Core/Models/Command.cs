namespace MarsRover.Core.Models;

/// <summary>
/// Interface for a command that can be executed by a rover.
/// </summary>
public interface ICommand
{
    void Execute();
}

/// <summary>
/// Move the rover forward by a number of units.
/// </summary>
public class MoveCommand : ICommand
{
    private IRover _rover;
    private int _units;

    public MoveCommand(IRover rover, int units)
    {
        _rover = rover;
        _units = units;
    }

    public void Execute()
    {
        _rover.Move(_units);
    }
}

/// <summary>
/// Rotate the rover left or right.
/// </summary>
public class RotateCommand : ICommand
{
    private IRover _rover;
    private TurnDirection _direction;

    public RotateCommand(IRover rover, TurnDirection direction)
    {
        _rover = rover;
        _direction = direction;
    }

    public void Execute()
    {
        _rover.Rotate(_direction);
    }
}

