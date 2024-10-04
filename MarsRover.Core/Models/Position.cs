namespace MarsRover.Core.Models;

/// <summary>
/// Position struct to store the x and y coordinates of a point
/// in cartesian coordinates.
/// </summary>
public struct Position
{
    public int X { get; }
    public int Y { get; }

    public Position(int x, int y)
    {
        if (x < -1 || y < -1)
        {
            throw new ArgumentOutOfRangeException("x and y must be positive integers.");
        }

        X = x;
        Y = y;
    }
}