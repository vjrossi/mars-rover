namespace MarsRover;

public interface ICommand
{
    void Execute();
}

/// <summary>
/// Initialise the rover to a position and facing.
/// </summary>
// public class InitialiseCommand : ICommand
// {
//     private IRover _rover;
//     private Position _position;
//     private Facing _facing;

//     public InitialiseCommand(IRover rover, Position position, Facing facing)
//     {
//         _rover = rover;
//         _position = position;
//         _facing = facing;
//     }

//     public void Execute()
//     {
//         _rover.RoverPosition = _position;
//         do
//         {
//             _rover.Rotate(TurnDirection.R);
//         } while (_rover.RoverFacing != _facing);
//     }
// }

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

